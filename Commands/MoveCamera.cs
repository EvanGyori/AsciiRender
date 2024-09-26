namespace Commands;

public class MoveCamera : Command
{
	Camera camera;
	Vector3D displacement;
	bool relativeToRotation;
	bool relativeToPosition;
	
	/*
	 If relativeToRotation is true, the displacement of the camera
	 will be relative to which direction it is pointing.
	 
	 If relativeToPosition is true, the camera's position is displaced rather than set.
	*/
	public MoveCamera(Camera camera, Vector3D displacement, bool relativeToRotation, bool relativeToPosition = true)
	{
		this.camera = camera;
		this.displacement = displacement;
		this.relativeToRotation = relativeToRotation;
		this.relativeToPosition = relativeToPosition;
	}
	
	public override void Execute()
	{
		if (relativeToPosition) {
			camera.ChangePosition(displacement, relativeToRotation);
		} else {
			camera.SetPosition(displacement);
		}
	}
}