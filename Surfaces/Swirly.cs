namespace Surfaces;
using Math = System.Math;

public class Swirly : Surface
{
	const double tau = 2 * Math.PI;
	public override Vector3D GetPosition(double u, double v, double time)
	{
		return new Vector3D(
			(4 + Math.Sin(tau * v) * Math.Sin(tau * v)) * Math.Sin(3 * Math.PI * v),
			Math.Sin(tau * v) * Math.Cos(tau * u) + 8 * v - 4,
			(4 + Math.Sin(tau * v) * Math.Sin(tau * u)) * Math.Cos(3 * Math.PI * v)
		);
	}
	
	public override Rect GetDomain()
	{
		return new Rect(0, 0, 1, 1);
	}
	
	public override int GetUSteps() => 250;
	public override int GetVSteps() => 250;
	
	public override Vector3D GetDerivativeWithU(double u, double v, double time)
	{
		return new Vector3D(
			tau * Math.Sin(tau * v) * Math.Sin(3 * Math.PI * v) * Math.Cos(tau * u),
			-tau * Math.Sin(tau * v) * Math.Sin(tau * u),
			tau * Math.Sin(tau * v) * Math.Cos(tau * u)
		);
	}
	
	public override Vector3D GetDerivativeWithV(double u, double v, double time)
	{
		return new Vector3D(
			3 * Math.PI * (4 + Math.Sin(tau * v) * Math.Sin(tau * u)) * Math.Cos(3 * Math.PI * v) + tau * Math.Cos(tau * v) * Math.Sin(tau * u) * Math.Sin(3 * Math.PI * v),
			tau * Math.Cos(tau * v) * Math.Cos(tau * u) + 8,
			-3 * Math.PI * (4 + Math.Sin(tau * v) * Math.Sin(tau * u)) * Math.Sin(3 * Math.PI * v) + tau * Math.Cos(tau * v) * Math.Sin(tau * u) * Math.Cos(3 * Math.PI * v)
		);
	}
}