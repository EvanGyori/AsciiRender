using static System.Console;
using Decs = Surfaces.Decorators;

Vector3D sunDirection = new(1.0, -1.0, -1.0);
sunDirection.Normalize();
Manager game = new(sunDirection, 5.0,
	new Decs.Position(new Vector3D(5.0, 5.0, 5.0), new Decs.Spinning(1, 1, new Surfaces.Donut(2.5, 1))),
	new Decs.Position(new Vector3D(17.0, 5.0, 5.0), new Decs.Spinning(0, 1, new Surfaces.Swirly()))
	);
	
game.Run();