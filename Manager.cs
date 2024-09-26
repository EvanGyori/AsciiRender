using static System.Console;
using System.Globalization;
using Math = System.Math;
using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;

// Handles the AsciiRender program
// Supply the surfaces and use the Run method
public class Manager
{
	Vector3D sunDirection;
	Camera camera;
	Surface[] surfaces;
	
	Renderer renderer;
	Controller controller;
	double FPSLimit = 0;
	BoolBox showHelp = new(false);
	BoolBox showDebug = new(true);
	
	// sunDirection must be a unit vector pointing in the direction of the sun.
	public Manager(Vector3D sunDirection, Camera camera, params Surface[] surfaces)
	{
		this.sunDirection = sunDirection;
		this.surfaces = surfaces;
		renderer = new Renderer(sunDirection, camera);
		this.camera = camera;
		
		controller = new Controller(camera, showHelp, showDebug);
	}
	
	// Prints the surfaces in an infinite loop
	public void Run()
	{
		Clear();
		while (true)
			Update();
	}
	
	// Handles each frame
	void Update()
	{
		DateTime initialTime = DateTime.Now;
		controller.Update();
		CursorVisible = false;
		
		if (!showHelp.value) {
			DrawScreen();
			if (showDebug.value)
				DrawDebug(initialTime);
		}
		
		if (FPSLimit > 0)
			LimitFPS(initialTime);
	}
	
	// Pauses the program temporarily to limit FPS
	void LimitFPS(DateTime initialTime)
	{
		TimeSpan dt = DateTime.Now.Subtract(initialTime);
		TimeSpan limit = TimeSpan.FromSeconds(1.0 / FPSLimit);
		if (dt.CompareTo(limit) < 0) {
			System.Threading.Thread.Sleep(limit.Subtract(dt));
		}
	}
	
	// Displays the surfaces
	void DrawScreen()
	{
		string screen = renderer.Render(surfaces);
		SetCursorPosition(0, 0);
		WriteLine(screen);
	}
	
	// Displays debug info such as FPS
	void DrawDebug(DateTime initialTime)
	{
		// FPS
		TimeSpan dt = DateTime.Now.Subtract(initialTime);
		int FPS = (int)Math.Ceiling(1.0 / dt.TotalSeconds);
		Write("FPS: " + FPS);
		if (FPSLimit > 0)
			Write(" limited at " + FPSLimit);
		
		// Camera
		double FOVInDegrees = Math.Floor(10 * camera.GetFOV() * 180 / Math.PI) / 10;
		Write("\tFOV: " + FOVInDegrees);
		WriteLine();
		
		Write("Press H for help");
	}
}