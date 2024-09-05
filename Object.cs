// Classes that implement the Object interface are parametric surfaces that can be used for calculations or drawn
public abstract class Object
{
	// x, y, and z components of the surface parameterized by u and v.
	public abstract Vector3D GetPosition(double u, double v, double time);
	
	// Returns the uv plane of the parametric surface
	public abstract Rect GetDomain();
	
	// Returns the number of steps the u portion of the uv plane needs to be discretized to
	public abstract int GetUSteps();
	
	// Returns the number of steps the v portion of the uv plane needs to be discretized to
	public abstract int GetVSteps();
	
	// Returns the partial derivative at a point with respect to u
	public abstract Vector3D GetDerivativeWithU(double u, double v, double time);

	// Returns the partial derivative at a point with respect to v
	public abstract Vector3D GetDerivativeWithV(double u, double v, double time);
	
	// Returns the unit normal vector for some point on the surface determined by the parameters and time.
	public Vector3D GetNormal(double u, double v, double time)
	{
		Vector3D normal = Vector3D.CrossProduct(
			GetDerivativeWithU(u, v, time),
			GetDerivativeWithV(u, v, time));
		normal.Normalize();
		return normal;
	}
}