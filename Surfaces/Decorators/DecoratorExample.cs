namespace Surfaces.Decorators;

public class Example : Decorator
{
	public Example(Surface next) : base(next)
	{	
	}
	
	public override int GetUSteps() => 1 + base.GetUSteps();
}