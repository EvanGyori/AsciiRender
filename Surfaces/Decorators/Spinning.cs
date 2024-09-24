namespace Surfaces.Decorators;
using Math = System.Math;

public class Spinning : Rotation
{
	double yzRate, xzRate, xyRate;
	
	public Spinning(double yzRate, double xzRate, double xyRate, Surface next) : base(0, 0, 0, next)
	{
		this.yzRate = yzRate;
		this.xzRate = xzRate;
		this.xyRate = xyRate;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		base.SetAngles(yzRate * time, xzRate * time, xyRate * time);
		return base.GetPosition(u, v, time);
	}
}