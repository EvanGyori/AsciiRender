namespace Surfaces;
using Math = System.Math;

public class Plane : Surface
{
	double width, height;
	
	public Plane(double width, double height)
	{
		this.width = width;
		this.height = height;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return new Vector3D(
			width * u - width / 2,
			height * v - height / 2,
			0
		);
	}
	
	public override Rect GetDomain() => new(0, 0, 1, 1);
	
	public override int GetUSteps() => (int)Math.Floor(width);
	public override int GetVSteps() => (int)Math.Floor(height);
	
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return new Vector3D(
			width,
			0,
			0
		);
	}
	
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return new Vector3D(
			0,
			height,
			0
		);
	}
}