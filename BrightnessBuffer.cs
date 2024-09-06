/*
 Stores a buffer of pixels where each pixel contains a brightness
 and a z coordinate. Write points to the buffer and use ToString
 to see all the points that are not blocked by other points.
*/
public class BrightnessBuffer
{
	Pixel[,] buffer;
	
	// Contains which characters to swap for brightness levels.
	// Make sure the minBrightness is in descending order
	static readonly BrightnessLevel[] levels = {
		new(0.5, 'a'),
		new(0.0, ' ')
	};
	
	// Initializes a buffer of size width by height. Each pixel is defaulted to a brightness of zero on the z plane
	public BrightnessBuffer(int width, int height)
	{
		buffer = new Pixel[width, height];
	}
	
	// Sets a pixel at a point to a brightness
	public void SetPixel(int x, int y, double z, double brightness)
	{
		buffer[x, y].brightness = brightness;
		buffer[x, y].z = z;
	}
	
	// Returns whether the given coordinates are in the range in be in the buffer
	public bool IsPixelInBoundaries(int x, int y)
	{
		return (0 <= x && x < buffer.GetLength(0)) && (0 <= y && y < buffer.GetLength(1));
	}
	
	// Returns whether there is another pixel in front and would be blocking the view of the given pixel
	public bool IsPixelBlocked(int x, int y, double z)
	{
		// z values of 0 indicate an unset pixel
		return buffer[x, y].z > 0 && buffer[x, y].z <= z && z > 0;
	}
	
	public override string ToString()
	{
		int width = buffer.GetLength(0);
		int height = buffer.GetLength(1);
		// The additional height is for the end line characters for each row
		char[] strBuilder = new char[width * height + height];
		
		// i keeps track of the current array element to write to
		int i = 0;
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				foreach (var level in levels) {
					if (buffer[x, y].brightness >= level.minBrightness) {
						strBuilder[i] = level.character;
						break;
					}
				}
				i++;
			}
			strBuilder[i] = '\n';
			i++;
		}
		
		return new string(strBuilder);
	}
	
	struct Pixel
	{
		public double brightness;
		public double z;
	}
	
	// Stores the character to use for each brightness level
	struct BrightnessLevel
	{
		public double minBrightness;
		public char character;
		
		public BrightnessLevel(double minBrightness, char character)
		{
			this.minBrightness = minBrightness;
			this.character = character;
		}
	}
}