namespace Commands;

public class ClearConsole : Command
{
	public override void Execute()
	{
		System.Console.Clear();
	}
}