namespace Surfaces;
using Math = System.Math;

public class Cuboid : Surface
{
	const double PI = System.Math.PI;
	
	double width, height, length; // x, y, z
	Surface top, bottom, front, back, right, left;
	
	public Cuboid(double width, double height, double length)
	{
		this.width = width;
		this.height = height;
		this.length = length;
		
		top = new Decorators.Position(new Vector3D(0, -height / 2, 0), 
			new Decorators.Rotation(PI / 2, 0, 0,
			new Plane(width, length)));
		bottom = new Decorators.Position(new Vector3D(0, height / 2, 0),
			new Decorators.Rotation(-PI / 2, 0, 0,
			new Plane(width, length)));
		front = new Decorators.Position(new Vector3D(0, 0, -length / 2),
			new Decorators.Rotation(PI, 0, 0,
			new Plane(width, height)));
		back = new Decorators.Position(new Vector3D(0, 0, length / 2),
			new Plane(width, height));
		right = new Decorators.Position(new Vector3D(width / 2, 0, 0),
			new Decorators.Rotation(0, -PI / 2, 0,
			new Plane(length, height)));
		left = new Decorators.Position(new Vector3D(-width / 2, 0, 0),
			new Decorators.Rotation(0, PI / 2, 0,
			new Plane(length, height)));
	}
	
	public override Vector3D GetPosition(double u, double v, double time)
	{
		switch((int)Math.Floor(u))
		{
			case 0:
				return top.GetPosition(u, v, time);
			case 1:
				return bottom.GetPosition(u - 1, v, time);
			case 2:
				return front.GetPosition(u - 2, v, time);
			case 3:
				return back.GetPosition(u - 3, v, time);
			case 4:
				return right.GetPosition(u - 4, v, time);
			case 5:
			case 6:
				return left.GetPosition(u - 5, v, time);
		}
		
		// TODO Error
		return new Vector3D(0, 0, 0);
	}
	
	public override Rect GetDomain() => new(0, 0, 6, 1);
	
	public override int GetUSteps() => 10 * 6 * (int)Math.Floor(Math.Max(width, length));
	public override int GetVSteps() => 10 * (int)Math.Floor(Math.Max(height, length));
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		switch((int)Math.Floor(u))
		{
			case 0:
				return top.GetDerivativeWithU(u, v, time);
			case 1:
				return bottom.GetDerivativeWithU(u - 1, v, time);
			case 2:
				return front.GetDerivativeWithU(u - 2, v, time);
			case 3:
				return back.GetDerivativeWithU(u - 3, v, time);
			case 4:
				return right.GetDerivativeWithU(u - 4, v, time);
			case 5:
			case 6:
				return left.GetDerivativeWithU(u - 5, v, time);
		}
		
		// TODO Error
		return new Vector3D(0, 0, 0);
	}
	
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		switch((int)Math.Floor(u))
		{
			case 0:
				return top.GetDerivativeWithV(u, v, time);
			case 1:
				return bottom.GetDerivativeWithV(u - 1, v, time);
			case 2:
				return front.GetDerivativeWithV(u - 2, v, time);
			case 3:
				return back.GetDerivativeWithV(u - 3, v, time);
			case 4:
				return right.GetDerivativeWithV(u - 4, v, time);
			case 5:
			case 6:
				return left.GetDerivativeWithV(u - 5, v, time);
		}
		
		// TODO Error
		return new Vector3D(0, 0, 0);
	}
}