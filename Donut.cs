namespace Surfaces;
using Math = System.Math;

public class Donut : Surface
{
	double radius;
	double thickness;
	Vector3D position;
	
	public Donut(Vector3D position, double radius, double thickness)
	{
		this.radius = radius;
		this.thickness = thickness;
		this.position = position;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return new Vector3D(
			radius * Math.Cos(u) + thickness * Math.Cos(v) * Math.Cos(u) + position.GetX(),
			radius * Math.Sin(u) + thickness * Math.Cos(v) * Math.Sin(u) + position.GetY(),
			thickness * Math.Sin(v) + position.GetZ()
		);
	}
	
	public override Rect GetDomain()
	{
		return new Rect(0, 0, 2 * Math.PI, 2 * Math.PI);
	}
	
	public override int GetUSteps() => 100;
	public override int GetVSteps() => 100;
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return new Vector3D(
			-1 * radius * Math.Sin(u) - thickness * Math.Cos(v) * Math.Sin(u),
			radius * Math.Cos(u) + thickness * Math.Cos(v) * Math.Cos(u),
			0
		);
	}
	
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return new Vector3D(
			-1 * thickness * Math.Sin(v) * Math.Cos(u),
			-1 * thickness * Math.Sin(v) * Math.Sin(u),
			thickness * Math.Cos(v)
		);
	}
}