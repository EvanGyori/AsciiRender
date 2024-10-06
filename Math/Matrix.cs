using Math = System.Math;

public class Matrix
{
	double[,] entries;

	public Matrix(int numRows, int numColumns)
	{	
		entries = new double[numRows, numColumns];
	}

	public void SetEntry(int row, int column, double value)
	{
		entries[row, column] = value;
	}

	public static Matrix operator*(Matrix lhs, Matrix rhs)
	{
		if (lhs.entries.GetLength(1) != rhs.entries.GetLength(0)) {
			throw new System.ArgumentException("The number of columns in the left matrix must match the number of rows in the right matrix to do matrix multiplication");
		}

		Matrix output = new(lhs.entries.GetLength(0), rhs.entries.GetLength(1));
		for (int i = 0; i < output.entries.GetLength(0); i++) {
			for (int j = 0; j < output.entries.GetLength(1); j++) {
				for (int k = 0; k < lhs.entries.GetLength(1); k++) {
					output.entries[i, j] += lhs.entries[i, k] * rhs.entries[k, j];
				}
			}
		}

		return output;
	}

	public static Vector3D operator*(Matrix lhs, Vector3D transposedRhs)
	{
		if (lhs.entries.GetLength(0) != 3 || lhs.entries.GetLength(1) != 3)
			throw new System.ArgumentException("The matrix must be a 3x3 when multiplying with a 3d vector");

		return new Vector3D(
			lhs.entries[0, 0] * transposedRhs.GetX() + lhs.entries[0, 1] * transposedRhs.GetY() + lhs.entries[0, 2] * transposedRhs.GetZ(),
			lhs.entries[1, 0] * transposedRhs.GetX() + lhs.entries[1, 1] * transposedRhs.GetY() + lhs.entries[1, 2] * transposedRhs.GetZ(),
			lhs.entries[2, 0] * transposedRhs.GetX() + lhs.entries[2, 1] * transposedRhs.GetY() + lhs.entries[2, 2] * transposedRhs.GetZ()
		);
	}

	public static Matrix XAxisRotation(double yzAngle)
	{
		// Precompute cos and sin so that they only need to be calculated once
		double cos = Math.Cos(yzAngle);
		double sin = Math.Sin(yzAngle);
		Matrix output = new(3, 3);
		output.entries = new double[3,3] {
			{1, 0, 0},
			{0, cos, -sin},
			{0, sin, cos}
		};
		return output;
	}

	public static Matrix YAxisRotation(double xzAngle)
	{
		double cos = Math.Cos(xzAngle);
		double sin = Math.Sin(xzAngle);
		Matrix output = new(3, 3);
		output.entries = new double[3,3] {
			{cos, 0, -sin},
			{0, 1, 0},
			{sin, 0, cos}
		};
		return output;
	}

	public static Matrix ZAxisRotation(double xyAngle)
	{
		double cos = Math.Cos(xyAngle);
		double sin = Math.Sin(xyAngle);
		Matrix output = new(3, 3);
		output.entries = new double[3,3] {
			{cos, -sin, 0},
			{sin, cos, 0},
			{0, 0, 1}
		};
		return output;
	}

	public static Matrix XYZRotation(Vector3D rotation)
	{
		// The order of transformations in the matrix multiplication is right to left
		return ZAxisRotation(rotation.GetZ()) * YAxisRotation(rotation.GetY()) * XAxisRotation(rotation.GetX());
	}
}
