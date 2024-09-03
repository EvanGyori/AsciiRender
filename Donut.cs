namespace Objects;

public class Donut : Object
{
	public double GetX(double u, double v, double time)
	{
		return u;
	}
	
	public double GetY(double u, double v, double time)
	{
		return v;
	}
	
	public double GetZ(double u, double v, double time)
	{
		return 1;
	}
	
	public Rect GetDomain()
	{
		return new Rect(0, 0, 10, 10);
	}
	
	public int GetUSteps() => 10;
	public int GetVSteps() => 10;
}