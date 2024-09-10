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
	double FPSLimit = 20;
	Surface[] surfaces;
	Renderer renderer;
	
	// sunDirection must be a unit vector pointing in the direction of the sun.
	public Manager(Vector3D sunDirection, params Surface[] surfaces)
	{
		this.sunDirection = sunDirection;
		this.surfaces = surfaces;
		renderer = new Renderer(sunDirection);
	}
	
	// Prints the surfaces in an infinite loop
	public void Run()
	{
		while (true)
		{
			Update();
		}
	}
	
	// Handles each frame
	void Update()
	{
		DateTime initialTime = DateTime.Now;
		Clear();
		Drawsurfaceects();
		DrawDebug(initialTime);
		if (FPSLimit > 0)
			LimitFPS(initialTime);
	}
	
	void Drawsurfaceects()
	{
		WriteLine(renderer.Render(surfaces, 10, 10, 1.0));
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
	
	// Displays debug info such as FPS
	void DrawDebug(DateTime initialTime)
	{
		TimeSpan dt = DateTime.Now.Subtract(initialTime);
		int FPS = (int)Math.Ceiling(1.0 / dt.TotalSeconds);
		int limitedFPS = Math.Min(FPS, (int)Math.Floor(FPSLimit));
		WriteLine("FPS: " + FPS + "  limited FPS: " + limitedFPS);
	}
}