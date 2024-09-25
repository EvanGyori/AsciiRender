public class Camera
{
	double pixelsPerUnitLength;
	double FOVFactor;
	Vector3D position;
	Vector3D rotation;
	
	public Camera(double pixelsPerUnitLength, double FOV, Vector3D position, Vector3D rotation)
	{
		this.pixelsPerUnitLength = pixelsPerUnitLength;
		this.FOVFactor = ComputeFOVFactor(FOV);
		this.position = position;
		this.rotation = rotation;
	}
	
	// Moves a point based on how the camera is set up
	public Vector3D ApplyView(Vector3D point)
	{
		return ApplyScaling(ApplyPerspectiveView(ApplyRotation(ApplyPosition(point))));
	}
	
	// Moves a point based on camera's position
	Vector3D ApplyPosition(Vector3D point)
	{
		// Substract position because when the camera moves in the +x direction,
		// in reality, everything is moving in the -x direction while the camera remains stationary
		return point - position;
	}
	
	// Moves a point based on camera's rotation
	Vector3D ApplyRotation(Vector3D point)
	{
		// -1 because everything rotates opposite to the
		// camera to make the camera appear as rotating the right direction
		point.RotateXYZ(rotation * -1);
		return point;
	}
	
	// Applies a perspective view opposed to an ordinary
	// orthographic view so that the camera acts like our eyes
	Vector3D ApplyPerspectiveView(Vector3D point)
	{
		return new Vector3D(
			FOVFactor * point.GetX() / point.GetZ(),
			FOVFactor * point.GetY() / point.GetZ(),
			point.GetZ()
		);
	}
	
	Vector3D ApplyScaling(Vector3D point)
	{
		return new Vector3D(
			point.GetX() * pixelsPerUnitLength,
			point.GetY() * pixelsPerUnitLength,
			point.GetZ()
		);
	}
	
	// Returns the factor used in the perspective equation for the given FOV in radians
	double ComputeFOVFactor(double FOV)
	{
		return 1 / System.Math.Tan(FOV / 2);
	}
}