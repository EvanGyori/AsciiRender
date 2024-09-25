namespace Surfaces.Decorators;
using Math = System.Math;

public class Rotation : Decorator
{
	Vector3D angles;
	
	public Rotation(Vector3D angles, Surface next) : base(next)
	{
		this.angles = angles;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		Vector3D position = base.GetPosition(u, v, time);
		position.RotateXYZ(angles);
		return position;
	}
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		// The derivative of the rotation is obtained by just passing the derivative since the rotations
		// are linear in addition of the functions
		Vector3D derivative = base.GetDerivativeWithU(u, v, time);
		derivative.RotateXYZ(angles);
		return derivative;
	}

	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		Vector3D derivative = base.GetDerivativeWithV(u, v, time);
		derivative.RotateXYZ(angles);
		return derivative;
	}
	
	public void SetAngles(Vector3D angles)
	{
		this.angles = angles;
	}
}