namespace Surfaces.Decorators;
using Math = System.Math;

public class Rotation : Decorator
{
	double yzAngle, xzAngle, xyAngle;
	
	public Rotation(double yzAngle, double xzAngle, double xyAngle, Surface next) : base(next)
	{
		SetAngles(yzAngle, xzAngle, xyAngle);
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return RotateAboutZAxis(RotateAboutYAxis(RotateAboutXAxis(base.GetPosition(u, v, time))));
	}
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		// The derivative of the rotation is obtained by just passing the derivative since the rotations
		// are linear in addition of the functions
		return RotateAboutZAxis(RotateAboutYAxis(RotateAboutXAxis(base.GetDerivativeWithU(u, v, time))));
	}

	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return RotateAboutZAxis(RotateAboutYAxis(RotateAboutXAxis(base.GetDerivativeWithV(u, v, time))));
	}
	
	public void SetAngles(double yzAngle, double xzAngle, double xyAngle)
	{
		this.yzAngle = yzAngle;
		this.xzAngle = xzAngle;
		this.xyAngle = xyAngle;
	}
	
	Vector3D RotateAboutXAxis(Vector3D position)
	{
		return new Vector3D(
			position.GetX(),
			position.GetY() * Math.Cos(yzAngle) - position.GetZ() * Math.Sin(yzAngle),
			position.GetY() * Math.Sin(yzAngle) + position.GetZ() * Math.Cos(yzAngle)
		);
	}
	
	Vector3D RotateAboutYAxis(Vector3D position)
	{
		return new Vector3D(
			position.GetX() * Math.Cos(xzAngle) - position.GetZ() * Math.Sin(xzAngle),
			position.GetY(),
			position.GetX() * Math.Sin(xzAngle) + position.GetZ() * Math.Cos(xzAngle)
		);
	}
	
	Vector3D RotateAboutZAxis(Vector3D position)
	{
		return new Vector3D(
			position.GetX() * Math.Cos(xyAngle) - position.GetY() * Math.Sin(xyAngle),
			position.GetX() * Math.Sin(xyAngle) + position.GetY() * Math.Cos(xyAngle),
			position.GetZ()
		);
	}
}