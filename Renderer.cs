// Takes in objects and outputs a string of ascii characters resembling the objects rendered
public class Renderer
{
	readonly Vector3D sunDirection;
	
	/*
	 sunDirection must be a normal vector pointing from the center to where the sun is at
	 to determine the direction of light.
	*/
	public Renderer(Vector3D sunDirection)
	{
		this.sunDirection = sunDirection;
	}
	
	/*
	 Returns a string of a screen to which the given objs were rendered to.
	 
	 screenWidth and screenHeight are the number of characters.
	 pixelsPerUnitLength determines the level of resolution
	*/
	public string Render(Object[] objs, int screenWidth, int screenHeight, double pixelsPerUnitLength)
	{
		BrightnessBuffer buffer = new(screenWidth, screenHeight);
		foreach (Object obj in objs) {
			WriteObjectToBuffer(buffer, obj, pixelsPerUnitLength);
		}
		
		return buffer.ToString();
	}
	
	/*
 	 Writes one object to the brightness buffer.
	 
	 Goes through a discrete set of u and v values in the object surface's domain
	 to draw each pixel.
	*/
	void WriteObjectToBuffer(BrightnessBuffer buffer, Object obj, double pixelsPerUnitLength)
	{
		Rect domain = obj.GetDomain();
		int uSteps = obj.GetUSteps();
		int vSteps = obj.GetVSteps();
		double du = (domain.GetWidth() - domain.GetX()) / uSteps;
		double dv = (domain.GetHeight() - domain.GetY()) / vSteps;
		
		double u = domain.GetX();
		// Loop executed +1 time to include the end of the domain
		// so that the domain is closed
		for (int i = 0; i <= uSteps; i++) {
			double v = domain.GetY();
			for (int j = 0; j <= vSteps; j++) {
				WritePixelToBuffer(buffer, obj, u, v, pixelsPerUnitLength);
				v += dv;
			}
			u += du;
		}
	}
	
	// For specific u and v values, determines and writes a pixel of an object to the brightness buffer
	void WritePixelToBuffer(BrightnessBuffer buffer, Object obj, double u, double v, double pixelsPerUnitLength)
	{
		double time = 0.0; // TODO get time
		Vector3D position = ApplyUniversalFunction(obj.GetPosition(u, v, time));
		// Map point to a pixel that can fit on the buffer
		int x = (int)System.Math.Floor(position.GetX() * pixelsPerUnitLength);
		int y = (int)System.Math.Floor(position.GetY() * pixelsPerUnitLength);
		
		// checks if pixel can be seen
		if (buffer.IsPixelInBoundaries(x, y) && !buffer.IsPixelBlocked(x, y, position.GetZ())) {
			double brightness = Vector3D.DotProduct(sunDirection, obj.GetNormal(u, v, time));
			buffer.SetPixel(x, y, position.GetZ(), brightness);
		}
	}
	
	/*
	 The function that is apply to all object positions.
	 
	 An orthographic perspective is achieved by returning the position as is.
	*/
	Vector3D ApplyUniversalFunction(Vector3D position)
	{
		return position; // No changes creates an orthographic perspective
	}
}