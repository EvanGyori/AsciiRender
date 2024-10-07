namespace Surfaces.Decorators;
using Math = System.Math;

public class Rotation : Decorator
{
	Matrix rotationMatrix;
	
	public Rotation(Vector3D angles, Surface next) : base(next)
	{
		SetAngles(angles);
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return rotationMatrix * base.GetPosition(u, v, time);
	}
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		// The derivative of the rotation is obtained by just passing the derivative since the rotations
		// are linear in addition of the functions
		return rotationMatrix * base.GetDerivativeWithU(u, v, time);
	}

	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return rotationMatrix * base.GetDerivativeWithV(u, v, time);
	}
	
	public void SetAngles(Vector3D angles)
	{
		rotationMatrix = Matrix.XYZRotation(angles);	
	}
}
