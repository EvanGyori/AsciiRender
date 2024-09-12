namespace Surfaces.Decorators;
using Math = System.Math;

public class Spinning : Rotation
{
	public Spinning(Surface next) : base(0.0, 0.0, next)
	{
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		base.SetAngles(0.0, time);
		return base.GetPosition(u, v, time);
	}
}