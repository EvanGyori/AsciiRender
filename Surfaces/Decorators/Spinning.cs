namespace Surfaces.Decorators;
using Math = System.Math;

public class Spinning : Rotation
{
	Vector3D rotationRate;
	
	public Spinning(Vector3D rotationRate, Surface next) : base(new Vector3D(0, 0, 0), next)
	{
		this.rotationRate = rotationRate;
	}
	
	public override void Update(double time)
	{
		base.SetAngles(rotationRate * time);
		base.Update(time);
	}
}
