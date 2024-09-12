﻿using static System.Console;
using Decs = Surfaces.Decorators;

Vector3D sunDirection = new(1.0, -1.0, -1.0);
sunDirection.Normalize();
Manager game = new(sunDirection, 5.0,
	//new Decs.Position(new Vector3D(7.0, 3.0, 5.0), new Decs.Spinning(new Surfaces.Swirly()))
	new Decs.Position(new Vector3D(5.0, 5.0, 5.0), new Decs.Spinning(new Surfaces.Donut(3.0, 1.0))),
	new Decs.Position(new Vector3D(15.0, 5.0, 5.0), new Decs.Spinning(new Surfaces.Donut(3.0, 1.0)))
	);
	
game.Run();