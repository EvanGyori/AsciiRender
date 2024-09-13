namespace Surfaces.Decorators;
using Math = System.Math;

public class Spinning : Rotation
{
	double yzRate, xzRate;
	
	public Spinning(double yzRate, double xzRate, Surface next) : base(0.0, 0.0, next)
	{
		this.yzRate = yzRate;
		this.xzRate = xzRate;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		base.SetAngles(yzRate * time, xzRate * time);
		return base.GetPosition(u, v, time);
	}
}