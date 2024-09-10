using static System.Console;
using Decs = Surfaces.Decorators;

Vector3D sunDirection = new(0.0, 0.0, -1.0);
sunDirection.Normalize();
Manager game = new(sunDirection,
	new Surfaces.Donut(new Vector3D(5.0, 5.0, 5.0), 3.0, 1.0)
	);
	
game.Run();