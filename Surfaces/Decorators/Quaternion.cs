namespace Surfaces.Decorators;
using Math = System.Math;

public class Quaternion : Decorator
{
	Matrix rotationMatrix;

	public Quaternion(Vector3D axis, double angle, Surface next) : base(next)
	{
		rotationMatrix = GetQuaternion(axis, angle);
	}

	public override Vector3D GetPosition(double u, double v, double time)
	{
		return ApplyQuaternion(base.GetPosition(u, v, time));
	}

	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return ApplyQuaternion(base.GetDerivativeWithU(u, v, time));
	}

	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return ApplyQuaternion(base.GetDerivativeWithV(u, v, time));
	}

	public Vector3D ApplyQuaternion(Vector3D position)
	{
		Matrix homogeneousCoords = new Matrix(new double[4, 1] {
			{ position.GetX() },
			{ position.GetY() },
			{ position.GetZ() },
			{ 1 }
		});
		homogeneousCoords = rotationMatrix * homogeneousCoords;
		double x = homogeneousCoords.GetEntry(0, 0);
		double y = homogeneousCoords.GetEntry(1, 0);
		double z = homogeneousCoords.GetEntry(2, 0);
		double w = homogeneousCoords.GetEntry(3, 0);
		return new Vector3D(x / w, y / w, z / w);
	}

	public static Matrix GetQuaternion(Vector3D axis, double angle)
	{
		axis.Normalize();
		double cos = Math.Cos(angle / 2);
		double sin = Math.Sin(angle / 2);

		// axis should be of unit length
		Matrix lhs = new(new double[4, 4] {
			{ cos, 			-axis.GetZ() * sin, 	axis.GetY() * sin, 	axis.GetX() * sin},
			{ axis.GetZ() * sin, 	cos, 			-axis.GetX() * sin, 	axis.GetY() * sin},
			{ -axis.GetY() * sin, 	axis.GetX() * sin, 	cos, 			axis.GetZ() * sin},
			{ -axis.GetX() * sin, 	-axis.GetY() * sin, 	-axis.GetZ() * sin, 	cos}
		});

		Matrix rhs = new(new double[4, 4] {
			{ cos, 			-axis.GetZ() * sin, 	axis.GetY() * sin, 	-axis.GetX() * sin},
			{ axis.GetZ() * sin, 	cos, 			-axis.GetX() * sin, 	-axis.GetY() * sin},
			{ -axis.GetY() * sin, 	axis.GetX() * sin, 	cos, 			-axis.GetZ() * sin},
			{ axis.GetX() * sin, 	axis.GetY() * sin, 	axis.GetZ() * sin, 	cos}
		});

		return lhs * rhs;
	}
}
