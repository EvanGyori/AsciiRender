# ASCII Render Engine

The program takes a different approach to rendering 3D objects from the conventional approach of using rays from the camera, or whatever the conventional approach is. The way it works requires that every 3D object be described by a parametric surface equation. Refer to [Details](#details) for the juicy math. Refer to [Usage](#usage) to run the program or add in your own cool shapes.

## Features
### Spinning Donut
[Equation](https://www.desmos.com/3d/hlziur9zvc)

![](Photos/SpinningDonut.gif)

### Any Parametric Surface
[Equation for Swirly Thing](https://www.desmos.com/3d/x9zww0oxcp) (Not my equation)

![](Photos/VariousSurfaces.png)

### Perspective View
Objects get smaller as they get further away which causes effects such as in a hallway where the walls seem to get closer at far distances.

![](Photos/DonutAisle.png)

### Moveable Camera
Pressing various keys moves the camera and adjusts other environment settings. Press H while in the program to see all the keybinds.

![](Photos/MovingCamera.gif)

### Backface culling
The back of surfaces are culled so that when you go inside an object you dont see its surface. Not much reason for this, just wanted to figure out how to do it.

## Usage
### Windows or Mac (using visual studio)
Install visual studio if not already installed

Run `AsciiRender.csproj` using visual studio.

Go to Debug -> Start Debugging

### Linux (using dotnet)
Install dotnet, the following command works on my computer

```
sudo apt-get install -y dotnet-sdk-8.0
```

Run the following inside the repository
```
dotnet run
```

### Adding Stuff

#### New Surface

#### Decorators

## Details

### Simplified Class Diagram

### Resulting Class Diagram

### Math