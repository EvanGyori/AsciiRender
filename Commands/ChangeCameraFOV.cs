namespace Commands;

public class ChangeCameraFOV : Command
{
	Camera camera;
	double delta;
	
	public ChangeCameraFOV(Camera camera, double delta)
	{
			this.camera = camera;
			this.delta = delta;
	}
	
	public override void Execute()
	{
		camera.ChangeFOV(delta);
	}
}