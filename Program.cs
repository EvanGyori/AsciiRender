using static System.Console;
using Decs = Surfaces.Decorators;
const double PI = System.Math.PI;

Vector3D sunDirection = new(1, -1, -1);
sunDirection.Normalize();

Camera camera = new(PI / 2, new Vector3D(0, 0, 0), new Vector3D(0, 0, 0));

Surface[] surfaces;

// Spinning Donut
surfaces = [new Decs.Position(new Vector3D(0.0, 0.0, 20.0), new Decs.Spinning(new Vector3D(1, 1, 0), new Surfaces.Donut(8, 3)))];

// Spinning Cube
//surfaces = [new Decs.Position(new Vector3D(0, 0, 20), new Decs.Spinning(new Vector3D(1, 1, 0), new Surfaces.Cuboid(12, 12, 12)))];

// Swirly Thing and Donut
/*
surfaces = [
	new Decs.Position(new Vector3D(-7.0, 0.0, 10.0),
		new Decs.Spinning(new Vector3D(1, 1, 0),
			new Surfaces.Donut(5, 1))),
	new Decs.Position(new Vector3D(10.0, 0.0, 5.0),
		new Decs.Spinning(new Vector3D(0, 1, 0),
			new Surfaces.Swirly()))
];
*/

// Aisle of donuts
/*
int numDonuts = 10;
surfaces = new Surface[2 * numDonuts];
for (int i = 0; i < numDonuts; i++) {
	surfaces[2 * i] = new Decs.Position(new Vector3D(-9.0, 0.0, 5.0 + 15 * i),
						new Decs.Spinning(new Vector3D(0, -1, 0),
							new Surfaces.Donut(4, 1)));
	surfaces[2 * i + 1] = new Decs.Position(new Vector3D(9.0, 0.0, 5.0 + 15 * i),
							new Decs.Spinning(new Vector3D(0, 1, 0),
								new Surfaces.Donut(4, 1)));
}
*/

// Grid of Donuts
/*
int length = 5;
surfaces = new Surface[length * length];
for (int x = 0; x < length; x++) {
	for (int y = 0; y < length; y++) {
		surfaces[x + y * length] = new Decs.Position(new Vector3D(15 * (x - length / 2) , 15 * (y - length / 2), 5 * length),
										new Decs.Spinning(new Vector3D(0.5 * x, 0.5 * y, 0),
											new Surfaces.Donut(4, 1)));
	}
}
*/

Manager game = new(sunDirection, camera, surfaces);

game.Run();