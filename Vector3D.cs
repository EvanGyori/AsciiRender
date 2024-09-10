public struct Vector3D
{
	double x, y, z;
	
	public Vector3D(double x, double y, double z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public double GetX() => x;
	public double GetY() => y;
	public double GetZ() => z;
	
	public double GetLength()
	{
		return System.Math.Sqrt(x * x + y * y + z * z);
	}

	// Changes the length of the vector to one while maintaining its direction
	public void Normalize()
	{
		double length = GetLength();
		x /= length;
		y /= length;
		z /= length;
	}
	
	// Returns the cross product between vectors lhs and rhs
	public static Vector3D CrossProduct(Vector3D lhs, Vector3D rhs)
	{
		return new Vector3D(
			lhs.GetY() * rhs.GetZ() - lhs.GetZ() * rhs.GetY(),
			lhs.GetZ() * rhs.GetX() - lhs.GetX() * rhs.GetZ(),
			lhs.GetX() * rhs.GetY() - lhs.GetY() * rhs.GetX()
			);
	}
	
	// Returns the dot product between vectors lhs and rhs
	public static double DotProduct(Vector3D lhs, Vector3D rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}
	
	// Adds two vectors
	public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
	{
		return new Vector3D(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
	}
}