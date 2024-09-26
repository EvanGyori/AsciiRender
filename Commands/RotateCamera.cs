namespace Commands;

public class RotateCamera : Command
{
	Camera camera;
	Vector3D rotation;
	bool setRotation;
	
	/*
	 if setRotation is true, sets the camera's rotation rather than changing it by a delta
	*/
	public RotateCamera(Camera camera, Vector3D rotation, bool setRotation = false)
	{
		this.camera = camera;
		this.rotation = rotation;
		this.setRotation = setRotation;
	}
	
	public override void Execute()
	{
		if (setRotation) {
			camera.SetRotation(rotation);
		} else {
			camera.ChangeRotation(rotation);
		}
	}
}