using System.Diagnostics;
using TimeSpan = System.TimeSpan;
using DateTime = System.DateTime;
using Math = System.Math;

// Takes in surfaceects and outputs a string of ascii characters resembling the surfaceects rendered
public class Renderer
{
	readonly Vector3D sunDirection;
	double time;
	
	/*
	 sunDirection must be a normal vector pointing from the center to where the sun is at
	 to determine the direction of light.
	*/
	public Renderer(Vector3D sunDirection)
	{
		this.sunDirection = sunDirection;
	}
	
	/*
	 Returns a string of a screen to which the given surfaces were rendered to.
	 
	 screenWidth and screenHeight are the number of characters.
	 pixelsPerUnitLength determines the level of resolution
	*/
	public string Render(Surface[] surfaces, int screenWidth, int screenHeight, double pixelsPerUnitLength)
	{
		BrightnessBuffer buffer = new(screenWidth, screenHeight);
		time = GetTime();
		foreach (Surface surface in surfaces) {
			WriteSurfaceToBuffer(buffer, surface, pixelsPerUnitLength);
		}
		
		return buffer.ToString();
	}
	
	/*
 	 Writes one surface to the brightness buffer.
	 
	 Goes through a discrete set of u and v values in the surface surface's domain
	 to draw each pixel.
	*/
	void WriteSurfaceToBuffer(BrightnessBuffer buffer, Surface surface, double pixelsPerUnitLength)
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
				WritePixelToBuffer(buffer, surface, u, v, pixelsPerUnitLength);
				v += dv;
			}
			u += du;
		}
	}
	
	// For specific u and v values, determines and writes a pixel of an surface to the brightness buffer
	void WritePixelToBuffer(BrightnessBuffer buffer, Surface surface, double u, double v, double pixelsPerUnitLength)
	{
		Vector3D position = ApplyUniversalFunction(surface.GetPosition(u, v, time));
		// Map point to a pixel that can fit on the buffer
		int x = (int)System.Math.Floor(position.GetX() * pixelsPerUnitLength);
		int y = (int)System.Math.Floor(position.GetY() * pixelsPerUnitLength);
		
		// checks if pixel can be seen
		if (buffer.IsPixelInBoundaries(x, y) && !buffer.IsPixelBlocked(x, y, position.GetZ())) {
			double brightness = Math.Max(0.01, Vector3D.DotProduct(sunDirection, surface.GetNormal(u, v, time)));
			buffer.SetPixel(x, y, position.GetZ(), brightness);
		}
	}
	
	/*
	 The function that is apply to all surfaceect positions.
	 
	 An orthographic perspective is achieved by returning the position as is.
	*/
	Vector3D ApplyUniversalFunction(Vector3D position)
	{
		return position; // No changes creates an orthographic perspective
	}
	
	double GetTime()
	{
		TimeSpan timeElapsed = DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime);
		return timeElapsed.TotalSeconds;
	}
}