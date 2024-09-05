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
	
	// Returns a cross b. a X b
	public static Vector3D CrossProduct(Vector3D a, Vector3D b)
	{
		return new Vector3D(
			a.GetY() * b.GetZ() - a.GetZ() * b.GetY(),
			a.GetZ() * b.GetX() - a.GetX() * b.GetZ(),
			a.GetX() * b.GetY() - a.GetY() * b.GetX()
			);
	}
}