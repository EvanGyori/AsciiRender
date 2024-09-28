using System.Diagnostics;
using TimeSpan = System.TimeSpan;
using DateTime = System.DateTime;
using Math = System.Math;

// Takes in surfaceects and outputs a string of ascii characters resembling the surfaceects rendered
public class Renderer
{
	readonly Vector3D sunDirection;
	readonly Camera camera;
	double time;
	
	bool cullBackFaces = true;
	
	/*
	 sunDirection must be a normal vector pointing from the center to where the sun is at
	 to determine the direction of light.
	*/
	public Renderer(Vector3D sunDirection, Camera camera)
	{
		this.sunDirection = sunDirection;
		this.camera = camera;
	}
	
	/*
	 Returns a string of a screen to which the given surfaces were rendered to.
	 
	 screenWidth and screenHeight are the number of characters for the width and height of the output buffer.
	 Default value of zero using the terminal's max size
	*/
	public string Render(Surface[] surfaces, int screenWidth = 0, int screenHeight = 0)
	{
		if (screenWidth == 0)
			screenWidth = System.Console.WindowWidth - 1;
		if (screenHeight == 0)
			screenHeight = System.Console.WindowHeight - 4;
		
		BrightnessBuffer buffer = new(screenWidth, screenHeight);
		time = GetTime();
		foreach (Surface surface in surfaces) {
			WriteSurfaceToBuffer(buffer, surface);
		}
		
		return buffer.ToString();
	}
	
	/*
 	 Writes one surface to the brightness buffer.
	 
	 Goes through a discrete set of u and v values in the surface surface's domain
	 to draw each pixel.
	*/
	void WriteSurfaceToBuffer(BrightnessBuffer buffer, Surface surface)
	{
		Rect domain = surface.GetDomain();
		int uSteps = surface.GetUSteps();
		int vSteps = surface.GetVSteps();
		double du = (domain.GetWidth() - domain.GetX()) / uSteps;
		double dv = (domain.GetHeight() - domain.GetY()) / vSteps;
		
		double u = domain.GetX();
		// Loop executed +1 time to include the end of the domain
		// so that the domain is closed
		for (int i = 0; i <= uSteps; i++) {
			double v = domain.GetY();
			for (int j = 0; j <= vSteps; j++) {
				WritePixelToBuffer(buffer, surface, u, v);
				v += dv;
			}
			u += du;
		}
	}
	
	// For specific u and v values, determines and writes a pixel of an surface to the brightness buffer
	void WritePixelToBuffer(BrightnessBuffer buffer, Surface surface, double u, double v)
	{
		// Original position stored so that it does not need to be recalculated
		Vector3D position = surface.GetPosition(u, v, time);
		Vector3D positionFromView = camera.ApplyView(surface.GetPosition(u, v, time));
		// Map point to a pixel that can fit on the buffer
		int x = (int)System.Math.Floor(positionFromView.GetX()) + buffer.GetWidth() / 2;
		int y = (int)System.Math.Floor(positionFromView.GetY()) + buffer.GetHeight() / 2;
		
		// checks if pixel can be seen
		if (positionFromView.GetZ() > 0 && buffer.IsPixelInBoundaries(x, y)
				&& !buffer.IsPixelBlocked(x, y, positionFromView.GetZ())) {
			Vector3D normal = surface.GetNormal(u, v, time);
			if (!cullBackFaces || camera.IsFrontFace(position, normal)) {
				double brightness = ComputeBrightness(normal);
				buffer.SetPixel(x, y, positionFromView.GetZ(), brightness);
			}
		}
	}
	
	// Returns a brightness level in [0, 1] of a point on a surface
	double ComputeBrightness(Vector3D normal)
	{
		return Math.Max(0.08, Vector3D.DotProduct(sunDirection, normal));
	}
	
	double GetTime()
	{
		TimeSpan timeElapsed = DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime);
		return timeElapsed.TotalSeconds;
	}
}