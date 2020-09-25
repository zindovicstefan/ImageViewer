using System;

public class HSL
{
	private double hue;
	private double saturation;
	private double lightness;

	public double Hue
	{
		get
		{
			return hue;
		}
		set
		{
			hue = (value > 360) ? 360 : ((value < 0) ? 0 : value);
		}
	}

	public double Saturation
	{
		get
		{
			return saturation;
		}
		set
		{
			saturation = (value > 1) ? 1 : ((value < 0) ? 0 : value);
		}
	}

	public double Lightness
	{
		get
		{
			return lightness;
		}
		set
		{
			lightness = (value > 1) ? 1 : ((value < 0) ? 0 : value);
		}
	}

	public HSL(double h, double s, double l)
	{
		hue = (h > 360) ? 360 : ((h < 0) ? 0 : h);
		saturation = (s > 1) ? 1 : ((s < 0) ? 0 : s);
		lightness = (l > 1) ? 1 : ((l < 0) ? 0 : l);
	}

	public override bool Equals(object obj)
	{
		HSL hsl = obj as HSL;
		if (hsl == null || !this.GetType().Equals(obj.GetType()))
			return false;
		return this.Hue == hsl.Hue && this.Saturation == hsl.Saturation && this.Lightness == hsl.Lightness;
	}
}