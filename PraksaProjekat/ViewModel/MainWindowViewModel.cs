using PraksaProjekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace PraksaProjekat
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{

		private double lightness;
		public double Lightness
		{
			get { return lightness; }
			set
			{
				lightness = value;
				OnPropertyChanged("Lightness");
				imageProcessor.Lightness = value;
				imageProcessor.Process();
			}
		}

		private double saturation;
		public double Saturation
		{
			get { return saturation; }
			set
			{
				saturation = value;
				OnPropertyChanged("Saturation");
				imageProcessor.Saturation = value;
				imageProcessor.Process();
			}
		}

		private int hue;
		public int Hue
		{
			get { return hue; }
			set
			{
				hue = value;
				OnPropertyChanged("Hue");
				CalculateSaturationBrush();
				imageProcessor.Hue = value;
				imageProcessor.Process();
			}
		}

		private double contrast;
		public double Contrast
        {
            get { return contrast; }
            set
            {
				contrast = value;
				OnPropertyChanged("Contrast");
				imageProcessor.Contrast = value;
				imageProcessor.Process();
            }
        }

		private void CalculateSaturationBrush()
        {	
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new Point(0, 0);
			linearGradientBrush.EndPoint = new Point(1, 0);
			GradientStop startGS = new GradientStop();
			HSL hsl = new HSL(Hue, 0, 0.5);
			RGB rgb = ColorModelConverter.HSLtoRGB(hsl);
			startGS.Color = new Color() { R = (byte)rgb.Red, G = (byte)rgb.Green, B = (byte)rgb.Blue, A = 255 };
			startGS.Offset = 0.0;
			linearGradientBrush.GradientStops.Add(startGS);
			GradientStop endGS = new GradientStop();
			hsl = new HSL(Hue, 1, 0.5);
			rgb = ColorModelConverter.HSLtoRGB(hsl);
			endGS.Color = new Color() { R = (byte)rgb.Red, G = (byte)rgb.Green, B = (byte)rgb.Blue, A = 255 };
			endGS.Offset = 1.0;
			linearGradientBrush.GradientStops.Add(endGS);
			SaturationBrush = linearGradientBrush;
		}

		private Brush saturationBrush;
		public Brush SaturationBrush
        {
            get { return saturationBrush; }
            set
            {
				saturationBrush = value;
				OnPropertyChanged("SaturationBrush");
            }
        }

		public Brush HueBrush { get; set; }

		public ReleyCommand LoadImageCommand { get; set; }

		public ReleyCommand EditImageCommand { get; set; }

		public ReleyCommand RotateRight90Command { get; set; }

		public ReleyCommand RotateLeft90Command { get; set; }

		public ReleyCommand FlipVerticalCommand { get; set; }

		public ReleyCommand FlipHorizontalCommand { get; set; }

		public ReleyCommand SaveImageCommand { get; set; }

		public ReleyCommand ResetImageCommand { get; set; }

		private ObservableCollection<ImageWrapper> imageList;
		public ObservableCollection<ImageWrapper> ImageList
		{
			get { return imageList; }
			set { imageList = value; OnPropertyChanged("ImageList"); }
		}

		private Image image;
		public Image Image
		{
			get { return image; }
			set
			{
				image = value;
				OnPropertyChanged("Image");
			}
		}
		public string ImageFormat { get; set; }
		private ImageProcessor imageProcessor;
		private FileService fileService;

		public MainWindowViewModel()
		{
			CalculateSaturationBrush();
			fileService = new FileService();
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri("../../Icons/hue.png", UriKind.Relative);
			bitmapImage.EndInit();
			HueBrush = new ImageBrush() { ImageSource = bitmapImage };
			LoadImageCommand = new ReleyCommand(LoadImage, CanExecuteLoadImage);
			EditImageCommand = new ReleyCommand(EditImage, CanExecuteEditImage);
			RotateRight90Command = new ReleyCommand(RotateRight90, CanExecuteRotateRight90);
			RotateLeft90Command = new ReleyCommand(RotateLeft90, CanExecuteRotateLeft90);
			FlipVerticalCommand = new ReleyCommand(FlipVertical, CanExecuteFlipVertical);
			FlipHorizontalCommand = new ReleyCommand(FlipHorizontal, CanExecuteFlipHorizontal);
			SaveImageCommand = new ReleyCommand(SaveImage, CanExecuteSaveImage);
			ResetImageCommand = new ReleyCommand(ResetImage, CanExecuteResetImage);
		}


		private void LoadImage(object obj)
		{
			ImageList = new ObservableCollection<ImageWrapper>();

			var fileFormats = new string[] { "jpg", "jpeg", "png", "gif" };
			string[] filePaths = fileService.Load(fileFormats);
			foreach(string filePath in filePaths)
            {
				string fileName = Path.GetFileNameWithoutExtension(filePath);
				ImageWrapper imageWrapped = new ImageWrapper(filePath, fileName);
				ImageList.Add(imageWrapped);
			}
		}

		private void SaveImage(object obj)
		{
			fileService.Save(Image, ImageFormat);
		}

		private void EditImage(object obj)
		{
			imageProcessor = new ImageProcessor(Image);
			ImageEditor imageEditor = new ImageEditor(this);
			imageEditor.ShowDialog();
		}

		private void ResetImage(object obj)
		{
			imageProcessor.Reset();
			Hue = 0;
			Saturation = 0;
			Lightness = 0;
			Contrast = 0;
		}

		private void RotateRight90(object obj)
		{
			RotateTransform transform = new RotateTransform(90);
			imageProcessor.Transform(transform);
			imageProcessor.Process();
		}

		private void RotateLeft90(object obj)
		{
			RotateTransform transform = new RotateTransform(-90);
			imageProcessor.Transform(transform);
			imageProcessor.Process();
		}

		private void FlipVertical(object obj)
		{
			ScaleTransform transform = new ScaleTransform(1, -1, 0, 0);
			imageProcessor.Transform(transform);
			imageProcessor.Process();
		}

		private void FlipHorizontal(object obj)
		{
			ScaleTransform transform = new ScaleTransform(-1, 1, 0, 0);
			imageProcessor.Transform(transform);
			imageProcessor.Process();
		}

		private bool CanExecuteEditImage(object obj)
		{
			return true;
		}

		private bool CanExecuteLoadImage(object obj)
		{
			return true;
		}

		private bool CanExecuteSaveImage(object obj)
		{
			return true;
		}

		private bool CanExecuteResetImage(object obj)
		{
			return true;
		}

		private bool CanExecuteRotateRight90(object obj)
		{
			return true;
		}

		private bool CanExecuteRotateLeft90(object obj)
		{
			return true;
		}

		private bool CanExecuteFlipVertical(object obj)
		{
			return true;
		}

		private bool CanExecuteFlipHorizontal(object obj)
		{
			return true;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
