using NUnit.Framework;
using System;
using System.Collections;
using System.Drawing.Imaging;

namespace PraksaProjekat.UnitTests
{
	[TestFixture]
	public class ColorModelConverterTests
	{
		[TestCaseSource("RGBtoHSLSource")]
		public void RGBtoHSL_CorrectCalculation(RGB rgb, HSL expected)
		{
			HSL result = ColorModelConverter.RGBtoHSL(rgb);
			Assert.That(result, Is.EqualTo(expected));
		}
		[Test]
		public void RGBtoHSL_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => ColorModelConverter.RGBtoHSL(null));
		}

		static object[] RGBtoHSLSource =
		{
			new object[] { new RGB(13, 13, 13), new HSL(0, 0, 0.051) },
			new object[] { new RGB(20, 20, 15), new HSL(60, 0.143, 0.069) },
			new object[] { new RGB(33, 66, 99), new HSL(210, 0.5, 0.259) }
		};

		[TestCaseSource("HSLtoRGBSource")]
		public void HSLtoRGB_CorrectCalculation(HSL hsl, RGB expected)
		{
			RGB result = ColorModelConverter.HSLtoRGB(hsl);
			Assert.That(result, Is.EqualTo(expected));
		}

		[Test]
		public void HSLtoRGB_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => ColorModelConverter.HSLtoRGB(null));
		}

		static object[] HSLtoRGBSource =
		{
			new object[] { new HSL(80, 0.5, 0.5), new RGB(149, 191, 64)},
			new object[] { new HSL(222, 0.32, 0.43), new RGB(75, 96, 145)},
			new object[] { new HSL(156, 0.7, 0.2), new RGB(15, 87, 58)}
		};
	}
}
