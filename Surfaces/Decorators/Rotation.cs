namespace Surfaces.Decorators;
using Math = System.Math;

public class Rotation : Decorator
{
	double yzAngle, xzAngle;
	
	public Rotation(double yzAngle, double xzAngle, Surface next) : base(next)
	{
		this.yzAngle = yzAngle;
		this.xzAngle = xzAngle;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return RotateAboutYAxis(RotateAboutXAxis(base.GetPosition(u, v, time)));
	}
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		// The derivative of the rotation is obtained by just passing the derivative since the rotations
		// are linear in addition of the functions
		return RotateAboutYAxis(RotateAboutXAxis(base.GetDerivativeWithU(u, v, time)));
	}

	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return RotateAboutYAxis(RotateAboutXAxis(base.GetDerivativeWithV(u, v, time)));
	}
	
	public void SetAngles(double yzAngle, double xzAngle)
	{
		this.yzAngle = yzAngle;
		this.xzAngle = xzAngle;
	}
	
	Vector3D RotateAboutXAxis(Vector3D position)
	{
		return new Vector3D(
			position.GetX(),
			position.GetY() * Math.Cos(yzAngle) + position.GetZ() * Math.Sin(yzAngle),
			-1 * position.GetY() * Math.Sin(yzAngle) + position.GetZ() * Math.Cos(yzAngle)
		);
	}
	
	Vector3D RotateAboutYAxis(Vector3D position)
	{
		return new Vector3D(
			position.GetX() * Math.Cos(xzAngle) + position.GetZ() * Math.Sin(xzAngle),
			position.GetY(),
			-1 * position.GetX() * Math.Sin(xzAngle) + position.GetZ() * Math.Cos(xzAngle)
		);
	}
}