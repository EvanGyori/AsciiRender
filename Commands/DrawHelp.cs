namespace Commands;

// Displays keybindings and helpful information
public class DrawHelp : Command
{
	BoolBox showHelp;
	
	/*
	 The value of showHelp determines whether help dialog is shown
	 or the screen is just cleared on command execution
	*/
	public DrawHelp(BoolBox showHelp)
	{
		this.showHelp = showHelp;
	}
	
	public override void Execute()
	{
		System.Console.Clear();
		if (showHelp.value) {
			System.Console.WriteLine("""
				Press H to hide Help
			
				CAMERA
				Movement
					W - Forward
					A - Left
					S - Backwards
					D - Right
					
					Q - Up
					E - Down
					
					N - Reset position to origin
					
				Rotating
					I - Up
					J - Left
					K - Down
					L - Right
					
					U - Counterclockwise
					O - Clockwise
					
					M - Reset rotation to original
					
				Other
					Left Arrow  - Decrease FOV
					Right Arrow - Increase FOV
					
				-----------------------------------
				MISCELLANEOUS
					G - Toggle Debugging
				""");
		}
	}
}