# ASCII Render Engine

The program takes a different approach to rendering 3D objects from the conventional approach of using rays from the camera, or whatever the conventional approach is. The way it works requires that every 3D object be described by a parametric surface equation. Refer to [Details](#details) for the juicy math. Refer to [Usage](#usage) to run the program or add your own cool shapes.

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
The back of surfaces are culled so that when you go inside an object you don't see its surface. Not much reason for this, just wanted to do it.

## Usage
### Windows or Mac (using Visual Studio)
Install Visual Studio if not already installed

Run `AsciiRender.csproj` using Visual Studio.

Go to Debug -> Start Debugging

### Linux (using dotnet)
[Install dotnet](https://learn.microsoft.com/en-us/dotnet/core/install/linux)

Run the following inside the repository.
```
dotnet run
```

### Adding Stuff

#### New Surface
Create a subclass of the Surface class and override each abstract method. The GetUSteps and GetVSteps methods determine how many parts to discretive the domain in the GetDomain function. These two methods may be removed in the future but for now, tinker with it until the object is rendered in full with no holes.

Also, overriding two other methods requires knowing how to take a derivative.

Look at `Plane.cs` for a simple example.

Once the subclass is complete. pass an instance into the surfaces array in `Program.cs` so that it is shown in the world.

#### Decorators
In order to move, rotate, or do any modular things to surfaces, decorators are used.

Examples of their use can be seen in `Program.cs` and to create a new one requires subclassing Decorator.

## Details

### Simplified Class Diagram
This is the general structure of the classes.

![](Photos/ClassDiagram.png)

The dashed lines indicate a has-a relationship with the arrowhead. The solid lines indicate an inheritance from the class at the arrowhead.

### Math

#### Drawing Shapes

The domain of the parametric surface (a UV plane) is split into discrete steps. A loop iterates over these steps giving a (u, v) coordinate which gives a (x, y, z) coordinate by the surface equation. x and y are the screen coordinates and z is depth.

Now this pixel needs some brightness. The normal of the surface at a point can be found by taking the cross product with two lines tangent to the point. Let $\vec{r}(u, v)$ be a position function for the surface. Holding u constant at the point yields a line whose tangent is $\frac{\partial \vec{r}}{\partial v}$. Holding v constant yields $\frac{\partial \vec{r}}{\partial u}$. Thus the normal is $\frac{\partial \vec{r}}{\partial u} \times \frac{\partial \vec{r}}{\partial v}$.

The more a surface "points" towards the sun, the brighter it is. The normal is the direction the surface points. So a dot product between the sun vector and the unit normal vector yields a value between -1 and 1. Where 1 is looking directly at the sun and -1 is looking directly away. This value is used for brightness.

#### Camera

In reality, there is no camera, just a shifting of the whole world. When the camera moves left, the world shifts to the right. Similarly, camera rotations apply an opposite rotation to the world about the origin.

The perspective view is a bit more complicated. The final equation can be found [here](https://www.desmos.com/geometry/rupqiij9no).

We want to achieve a view that looks like the following:

![](Photos/FOVGraph.png)

On the y-axis is the depth, $z$, and on the x-axis is the original position, $x$. Everything in the highlighted area is a point to be included in our view. Essentially, the further away a point is --higher up the graph-- a wider range of x values is included in our view --we can see an entire building at a distance but only the wall when its in our face.

The equation for the graph is $c|x| \leq z$ where $c$ is some constant determining the slope of the lines. Changing $c$ changes the FOV.

![](Photos/BeautifulDrawing.png)

Then we have $c = \cot(\frac{\theta}{2})$.

Rearranging $c|x| \leq z$ yields $\frac{|x|\cot(\frac{\theta}{2})}{z} \leq 1$. Now let $x_f$ be the actual output position on the screen and $w$ be the width of the screen. We could set $|x_f| = \frac{|x|\cot(\frac{\theta}{2})}{z}$ which gives $|x_f| \leq 1$ but that would mean we only see $x$ values from -1 to 1. We actually see values from $-w$ to $w$. so first obtain $\frac{w|x|\cot(\frac{\theta}{2})}{z} \leq w$. This gives us the final equation:

$x_f = \frac{wx*\cot(\frac{\theta}{2})}{z}$

#### Rotations

I have heard of quaternions and hope to learn and implement them in the future. However, for now, rotations are done by first rotating a point about the x-axis, then y-axis, and then the z-axis using three separate angles. The following math applies to rotations about the z-axis but is identical for rotations about other axes.

Let $\vec{r} = x\hat{i} + y\hat{j} + z\hat{k}$ be the position vector pre-rotation. The $z$ coordinate will remain unchanged since we are rotating about the z-axis. To do a rotation, we can think about the positions changing, rotating each vector, making sure magnitude stays the same, and all that stuff. But an easier approach is thinking about the axes just rotating in the opposite direction. Then, when we move the axes back to their original orientation, we will see that the object is rotated.

Square object prior to rotation:

![](Photos/NoRotation.png)

New axes to base position vector on:

![](Photos/AxesRotation.png)

New axes in place of original axes so that the object has now rotated:

![](Photos/FinishedRotation.png)

For the rotated axes, let its unit vectors be $\hat{i'}$, $\hat{j'}$, and $\hat{k'}$.

![](Photos/ChangeOfBaseTriangleI.png)

From the image, we have $\hat{i} = \cos(\theta) \hat{i'} + \sin(\theta) \hat{j'}$.

![](Photos/ChangeOfBaseTriangleJ.png)

Again from the image: $\hat{j} = -\sin(\theta) \hat{i'} + \cos(\theta) \hat{j'}$.

And since $z$ is unchanged, $\hat{k} = \hat{k'}$.

Applying these equations to $\vec{r} = x\hat{i} + y\hat{j} + z\hat{k}$ and simplifying the result gives:

$\vec{r} = (x * \cos(\theta) - y * \sin(\theta)) \hat{i'} + (x * \sin(\theta) + y * \cos(\theta)) \hat{j'} + z \hat{k'}$

So, for example, the $x$ coordinate after the rotation is $x * \cos(\theta) - y * \sin(\theta)$ where $x$ and $y$ are the original coordinates.
