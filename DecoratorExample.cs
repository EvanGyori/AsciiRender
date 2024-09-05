namespace Objects.Decorators;

public class Example : Decorator
{
	public Example(Object next) : base(next)
	{	
	}
	
	public override int GetUSteps() => 1 + base.GetUSteps();
}