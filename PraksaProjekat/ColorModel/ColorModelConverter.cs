using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraksaProjekat
{
	public class ColorModelConverter
	{
		public static HSL RGBtoHSL(RGB rgb)
		{
			if (rgb == null)
				throw new ArgumentNullException();
			double h = 0, s = 0, l = 0;
			double r = (double)rgb.Red / 255;
			double g = (double)rgb.Green / 255;
			double b = (double)rgb.Blue / 255;

			double max = Math.Max(r, Math.Max(g, b));
			double min = Math.Min(r, Math.Min(g, b));

			// hue
			if (max == min)
			{
				h = 0; // undefined
			}
			else if (max == r)
			{
				h = (((g - b) / (max - min)) % 6) * 60;
			}
			else if (max == g)
			{
				h = 60.0 * (b - r) / (max - min) + 120.0;
			}
			else if (max == b)
			{
				h = 60.0 * (r - g) / (max - min) + 240.0;
			}

			// lightness
			l = (max + min) / 2.0;

			// saturation
			if (max == min)
			{
				s = 0;
			}
			else
			{
				s = (max - min) / (1 - Math.Abs(2 * l - 1));
			}
			h = Math.Round(h, 0);
			s = Math.Round(s, 3);
			l = Math.Round(l, 3);
			return new HSL(h, s, l);
		}

		public static RGB HSLtoRGB(HSL hsl)
		{
			if (hsl == null)
				throw new ArgumentNullException();
			double h = hsl.Hue;
			double s = hsl.Saturation;
			double l = hsl.Lightness;
			double c = (1 - Math.Abs(2 * l - 1)) * s;
			double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
			double m = l - c / 2;
			RGB rgb;
			int r, g, b;
			if (0 <= h && h < 60)
			{
				r = (int)Math.Round((c + m) * 255);
				g = (int)Math.Round((x + m) * 255);
				b = (int)Math.Round(m * 255);
				rgb = new RGB(r, g, b);
			}
			else if (60 <= h && h < 120)
			{
				r = (int)Math.Round((x + m) * 255);
				g = (int)Math.Round((c + m) * 255);
				b = (int)Math.Round(m * 255);
				rgb = new RGB(r, g, b);
			}
			else if (120 <= h && h < 180)
			{
				r = (int)Math.Round(m * 255);
				g = (int)Math.Round((c + m) * 255);
				b = (int)Math.Round((x + m) * 255);
				rgb = new RGB(r, g, b);
			}
			else if (180 <= h && h < 240)
			{
				r = (int)Math.Round(m * 255);
				g = (int)Math.Round((x + m) * 255);
				b = (int)Math.Round((c + m) * 255);
				rgb = new RGB(r, g, b);
			}
			else if (240 <= h && h < 300)
			{
				r = (int)Math.Round((x + m) * 255);
				g = (int)Math.Round(m * 255);
				b = (int)Math.Round((c + m) * 255);
				rgb = new RGB(r, g, b);
			}
			else
			{
				r = (int)Math.Round((c + m) * 255);
				g = (int)Math.Round(m * 255);
				b = (int)Math.Round((x + m) * 255);
				rgb = new RGB(r, g, b);
			}

			return rgb;
		}

	}
}
