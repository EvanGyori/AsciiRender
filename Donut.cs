namespace Objects;

public class Donut : Object
{
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return new Vector3D(u, v, 1);
	}
	
	public override Rect GetDomain()
	{
		return new Rect(0, 0, 10, 10);
	}
	
	public override int GetUSteps() => 10;
	public override int GetVSteps() => 10;
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return new Vector3D(1, 0, 0);
	}
	
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return new Vector3D(0, 1, 0);
	}
}