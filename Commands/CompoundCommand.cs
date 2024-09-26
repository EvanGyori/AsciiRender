// Command that executes any number of commands
public class CompoundCommand : Command
{
	// commands to execute
	Command[] commands;
	
	public CompoundCommand(params Command[] commands)
	{
		this.commands = commands;
	}
	
	public override void Execute()
	{
		foreach (Command command in commands) {
			command.Execute();
		}
	}
}