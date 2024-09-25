using static System.Console;
using Decs = Surfaces.Decorators;
const double PI = System.Math.PI;

Vector3D sunDirection = new(-1, -0.5, -1);
sunDirection.Normalize();

Camera camera = new(1, PI / 40, new Vector3D(0, 0, 0), new Vector3D(0, PI / 4, 0));

/*
Manager game = new(sunDirection, 5.0,
	new Decs.Position(new Vector3D(5.0, 5.0, 5.0), new Decs.Spinning(1, 1, new Surfaces.Donut(2.5, 1))),
	new Decs.Position(new Vector3D(17.0, 5.0, 5.0), new Decs.Spinning(0, 1, new Surfaces.Swirly()))
	);
*/

Manager game = new(sunDirection, camera,
	new Decs.Position(new Vector3D(0, 0, 10),
		new Decs.Spinning(new Vector3D(1, 1, 0),
			new Surfaces.Cuboid(10, 10, 10)))
	);
	
game.Run();