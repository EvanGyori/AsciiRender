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

	public void Normalize()
	{
		double length = GetLength();
		x /= length;
		y /= length;
		z /= length;
	}
}