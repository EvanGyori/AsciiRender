// Classes that implement the Object interface are parametric surfaces that can be used for calculations or drawn
public interface Object
{
	// x, y, and z components of the surface parameterized by u and v.
	public Vector3D getPosition(double u, double v, double t);
	
	// Returns the uv plane of the parametric surface
	public Rect GetDomain();
	
	// Returns the number of steps the u portion of the uv plane needs to be discretized to
	public int GetUSteps();
	
	// Returns the number of steps the v portion of the uv plane needs to be discretized to
	public int GetVSteps();
}