namespace Commands;

public class ToggleBoolean : Command
{
	BoolBox target;
	
	// target is the wrapped boolean value to toggle on command execution
	public ToggleBoolean(BoolBox target)
	{
		this.target = target;
	}
	
	public override void Execute()
	{
		target.value = !target.value;
	}
}