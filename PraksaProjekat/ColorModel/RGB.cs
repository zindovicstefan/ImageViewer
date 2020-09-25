public class RGB
{
	private byte red;
	private byte green;
	private byte blue;

	public byte Red
	{
		get
		{
			return red;
		}
		set
		{
			red = Truncate(value);
		}
	}

	public byte Green
	{
		get
		{
			return green;
		}
		set
		{
			green = Truncate(value);
		}
	}

	public byte Blue
	{
		get
		{
			return blue;
		}
		set
		{
			blue = Truncate(value);
		}
	}

	public RGB(int R, int G, int B)
	{
		red = Truncate(R);
		green = Truncate(G);
		blue = Truncate(B);
	}

    public RGB()
    {
    }

	public override bool Equals(object obj)
	{
		RGB rgb = obj as RGB;
		if (rgb == null || !this.GetType().Equals(obj.GetType()))
			return false;
		return this.Red == rgb.Red && this.Green == rgb.Green && this.Blue == rgb.Blue;
	}

	private byte Truncate(int value)
	{
		if (value < 0) value = 0;
		else if (value > 255) value = 255;
		return (byte)value;
	}
}
