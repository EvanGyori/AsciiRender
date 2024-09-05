namespace Objects.Decorators;

// Applies customizable functionality on top of a concrete object.
public abstract class Decorator : Object
{
	Object next;
	
	public Decorator(Object next)
	{
		this.next = next;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return next.GetPosition(u, v, time);
	}
	
	public override Rect GetDomain()
	{
		return next.GetDomain();
	}
	
	public override int GetUSteps()
	{
		return next.GetUSteps();
	}
	
	public override int GetVSteps()
	{
		return next.GetVSteps();
	}
	
	// Must be overriden if GetPosition is overriden
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return next.GetDerivativeWithU(u, v, time);
	}

	// Must be overriden if GetPosition is overriden
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return next.GetDerivativeWithV(u, v, time);
	}
}