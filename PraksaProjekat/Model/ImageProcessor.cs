using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace PraksaProjekat
{
	public class ImageProcessor
	{
		public Image Image { get; set; }
		private Image backupImage;
		private List<Transform> transforms;
		public double Hue { get; set; }
		public double Saturation { get; set; }
		public double Lightness { get; set; }
		public double Brightness { get; set; }
		public double Contrast { get; set; }
		public ImageProcessor(Image image)
		{
			Image = image;
			backupImage = new Image() { Source = image.Source };
			transforms = new List<Transform>();
		}
		public void Reset()
		{
			Image.Source = backupImage.Source;
			transforms = new List<Transform>();
		}
		public void Transform(Transform transform)
		{
			transforms.Add(transform);
		}
		public void Process()
		{
			Image.Source = backupImage.Source;
			foreach (Transform transform in transforms)
			{
				TransformedBitmap tb = new TransformedBitmap();
				tb.BeginInit();
				tb.Source = (BitmapSource)Image.Source;
				tb.Transform = transform;
				tb.EndInit();
				Image.Source = tb;
			}

			WriteableBitmap source = new WriteableBitmap((BitmapSource)Image.Source);
			WriteableBitmap dest = new WriteableBitmap((BitmapSource)Image.Source);

			try
			{
				dest.Lock();

				unsafe
				{
					IntPtr sourceBuffer = source.BackBuffer;
					IntPtr destBuffer = dest.BackBuffer;
					byte* sourcepbuffer = (byte*)sourceBuffer.ToPointer();
					byte* destpbuffer = (byte*)destBuffer.ToPointer();
					int Stride = dest.BackBufferStride;
					int height = dest.PixelHeight;
					Parallel.For(0, dest.PixelWidth, x =>
					{

						for (int y = 0; y < height; y++)
						{
							int loc = y * Stride + x * 4;

							RGB rgb = new RGB();
							rgb.Blue = sourcepbuffer[loc];
							rgb.Green = sourcepbuffer[loc + 1];
							rgb.Red = sourcepbuffer[loc + 2];

							HSL hsl = ColorModelConverter.RGBtoHSL(rgb);

							SetHue(hsl);
							SetLightness(hsl);
							SetSaturation(hsl);
							
							rgb = ColorModelConverter.HSLtoRGB(hsl);

							SetContrast(rgb);
							
							destpbuffer[loc] = rgb.Blue;
							destpbuffer[loc + 1] = rgb.Green;
							destpbuffer[loc + 2] = rgb.Red;
						}

					});
					dest.AddDirtyRect(new Int32Rect(0, 0, dest.PixelWidth, dest.PixelHeight));
				}

			}
			finally
			{
				dest.Unlock();
			}

			Image.Source = dest;
		}

		private void SetHue(HSL hsl)
		{
			if (Hue != 0)
			{
				hsl.Hue = Hue / 60 * 60 + hsl.Hue % 60;
			}
		}
		private void SetLightness(HSL hsl)
		{
			if (Lightness >= 0)
			{
				hsl.Lightness = (1 - hsl.Lightness) * Lightness / 100 + hsl.Lightness;
			}
			else
			{
				hsl.Lightness = hsl.Lightness + hsl.Lightness * Lightness / 100;
			}
		}
		private void SetSaturation(HSL hsl)
		{
			if (Saturation >= 0)
			{
				hsl.Saturation = (1 - hsl.Saturation) * Saturation / 100 + hsl.Saturation;
			}
			else
			{
				hsl.Saturation = hsl.Saturation + hsl.Saturation * Saturation / 100;
			}
		}
		private void SetContrast(RGB rgb)
		{
			double contrastCorrectionFactor = 259.0 * (Contrast + 255.0) / 255.0 / (259.0 - Contrast);
			int red = (int)(contrastCorrectionFactor * ((double)rgb.Red - 128) + 128);
			int green = (int)(contrastCorrectionFactor * ((double)rgb.Green - 128) + 128);
			int blue = (int)(contrastCorrectionFactor * ((double)rgb.Blue - 128) + 128);
			RGB temp = new RGB(red, green, blue);
			rgb.Red = temp.Red;
			rgb.Green = temp.Green;
			rgb.Blue = temp.Blue;
		}
	}
}
