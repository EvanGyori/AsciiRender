namespace Surfaces.Decorators;

public class Position : Decorator
{
	Vector3D position;
	
	public Position(Vector3D position, Surface next) : base(next)
	{
		this.position = position;
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return base.GetPosition(u, v, time) + position;
	}
}