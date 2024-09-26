public class Camera
{
	double FOV;
	double FOVFactor;
	Vector3D position;
	Vector3D rotation;
	
	public Camera(double FOV, Vector3D position, Vector3D rotation)
	{
		SetFOV(FOV);
		this.position = position;
		this.rotation = rotation;
	}
	
	// Moves a point based on how the camera is set up
	public Vector3D ApplyView(Vector3D point)
	{
		return ApplyPerspectiveView(ApplyRotation(ApplyPosition(point)));
	}
	
	public void SetPosition(Vector3D position)
	{
		this.position = position;
	}
	
	public void ChangePosition(Vector3D displacement, bool relativeToRotation)
	{
		if (relativeToRotation)
			displacement.RotateXYZ(rotation);
		position += displacement;
	}
	
	public void SetRotation(Vector3D rotation)
	{
		this.rotation = rotation;
	}
	
	public void ChangeRotation(Vector3D rotation)
	{
		this.rotation += rotation;
	}
	
	// Changes FOV by the amount, delta in radians. The FOV is clamped between 0 and 180 degrees.
	public void ChangeFOV(double delta)
	{
		SetFOV(System.Math.Clamp(FOV + delta, 0.01, System.Math.PI - 0.01));
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
	
	// Call this method when setting the FOV so that the FOVFactor is computed as well
	void SetFOV(double FOV)
	{
		this.FOV = FOV;
		FOVFactor = ComputeFOVFactor();
	}
	
	// Returns the factor used in the perspective equation for the given FOV in radians
	double ComputeFOVFactor()
	{
		return 1 / System.Math.Tan(FOV / 2);
	}
}