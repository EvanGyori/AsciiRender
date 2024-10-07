public struct Rect
{
	double x, y;
	double width, height;
	
	public Rect(double x, double y, double width, double height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}
	
	public double GetX() => x;
	public double GetY() => y;
	public double GetWidth() => width;
	public double GetHeight() => height;
}