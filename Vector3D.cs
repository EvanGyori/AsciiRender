using Math = System.Math;

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
	
	// Returns the length of the vector
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
			lhs.y * rhs.z - lhs.z * rhs.y,
			lhs.z * rhs.x - lhs.x * rhs.z,
			lhs.x * rhs.y - lhs.y * rhs.x
			);
	}
	
	// Returns the dot product between vectors lhs and rhs
	public static double DotProduct(Vector3D lhs, Vector3D rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}
	
	// Adds two vectors as expected
	public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
	{
		return new Vector3D(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
	}
	
	// Subtracts two vectors as expected
	public static Vector3D operator -(Vector3D lhs, Vector3D rhs)
	{
		return new Vector3D(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
	}
	
	// Multiplies a vector by a scalar as expected
	public static Vector3D operator *(Vector3D vector, double scalar)
	{
		return new Vector3D(scalar * vector.x, scalar * vector.y, scalar * vector.z);
	}
	
	// Rotates the vector around the x axis first, then y, and then the z axis.
	public void RotateXYZ(Vector3D rotation)
	{
		RotateAboutXAxis(rotation.x);
		RotateAboutYAxis(rotation.y);
		RotateAboutZAxis(rotation.z);
	}
	
	/* 
	 Rotates the vector around the x axis.
	 yzAngle is an angle made from the +y axis towards the +z axis.
	*/
	public void RotateAboutXAxis(double yzAngle)
	{
		double y_0 = y;
		y = y * Math.Cos(yzAngle) - z * Math.Sin(yzAngle);
		z = y_0 * Math.Sin(yzAngle) + z * Math.Cos(yzAngle);
	}
	
	/*
	 Rotates the vector around the y axis.
	 xzAngle is an angle made from the +x axis towards the +z axis.
	*/
	public void RotateAboutYAxis(double xzAngle)
	{
		double x_0 = x;
		x = x * Math.Cos(xzAngle) - z * Math.Sin(xzAngle);
		z = x_0 * Math.Sin(xzAngle) + z * Math.Cos(xzAngle);
	}
	
	/*
	 Rotates the vector around the z axis.
	 xyAngle is an angle made from the +x axis towards the +y axis.
	*/
	public void RotateAboutZAxis(double xyAngle)
	{
		double x_0 = x;
		x = x * Math.Cos(xyAngle) - y * Math.Sin(xyAngle);
		y = x_0 * Math.Sin(xyAngle) + y * Math.Cos(xyAngle);
	}
}