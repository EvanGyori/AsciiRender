using System.Collections.Generic;
using Keys = System.ConsoleKey;

/*
 Controller class handles console inputs while the program is running
*/
public class Controller
{
	Dictionary<System.ConsoleKey, Command> keyBindings = new();
	
	double moveSpeed = 1;
	double rotateSpeed = 0.1;
	double deltaFOV = System.Math.PI / 180;
	
	public Controller(Camera camera, BoolBox showHelp, BoolBox showDebug)
	{
		SetDefaultKeyBindings(camera, showHelp, showDebug);
	}
	
	// Checks and handles keyboard input
	public void Update()
	{
		if (System.Console.KeyAvailable) {
			var key = System.Console.ReadKey().Key;
			if (keyBindings.ContainsKey(key)) {
				keyBindings[key].Execute();
			}
		}
	}
	
	void SetDefaultKeyBindings(Camera camera, BoolBox showHelp, BoolBox showDebug)
	{
		////////////
		// CAMERA //
		////////////
		
		// Movement
		keyBindings.Add(Keys.W, new Commands.MoveCamera(camera, new Vector3D(0, 0, moveSpeed), true));
		keyBindings.Add(Keys.A, new Commands.MoveCamera(camera, new Vector3D(-moveSpeed, 0, 0), true));
		keyBindings.Add(Keys.S, new Commands.MoveCamera(camera, new Vector3D(0, 0, -moveSpeed), true));
		keyBindings.Add(Keys.D, new Commands.MoveCamera(camera, new Vector3D(moveSpeed, 0, 0), true));
		keyBindings.Add(Keys.Q, new Commands.MoveCamera(camera, new Vector3D(0, -moveSpeed, 0), true));
		keyBindings.Add(Keys.E, new Commands.MoveCamera(camera, new Vector3D(0, moveSpeed, 0), true));
		
		// Rotation
		keyBindings.Add(Keys.I, new Commands.RotateCamera(camera, new Vector3D(rotateSpeed, 0, 0)));
		keyBindings.Add(Keys.J, new Commands.RotateCamera(camera, new Vector3D(0, rotateSpeed, 0)));
		keyBindings.Add(Keys.K, new Commands.RotateCamera(camera, new Vector3D(-rotateSpeed, 0, 0)));
		keyBindings.Add(Keys.L, new Commands.RotateCamera(camera, new Vector3D(0, -rotateSpeed, 0)));
		keyBindings.Add(Keys.U, new Commands.RotateCamera(camera, new Vector3D(0, 0, -rotateSpeed)));
		keyBindings.Add(Keys.O, new Commands.RotateCamera(camera, new Vector3D(0, 0, rotateSpeed)));
		
		// Change FOV
		keyBindings.Add(Keys.LeftArrow, new Commands.ChangeCameraFOV(camera, -deltaFOV));
		keyBindings.Add(Keys.RightArrow, new Commands.ChangeCameraFOV(camera, deltaFOV));
		
		// Reset position to origin
		keyBindings.Add(Keys.N, new Commands.MoveCamera(camera, new Vector3D(0, 0, 0), false, false));
		
		// Reset rotation and FOV to default
		keyBindings.Add(Keys.M, new Commands.RotateCamera(camera, new Vector3D(0, 0, 0), true));
		
		///////////
		// OTHER //
		///////////
		
		// Toggle help, the order of command execution does matter
		// since DrawHelp relies on the value of showHelp
		keyBindings.Add(Keys.H, new Commands.CompoundCommand(
			new Commands.ToggleBoolean(showHelp),
			new Commands.DrawHelp(showHelp)));
		
		// Toggle debugging
		keyBindings.Add(Keys.G, new Commands.CompoundCommand(
			new Commands.ClearConsole(),
			new Commands.ToggleBoolean(showDebug)));
	}
}