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
		new(0.9999, '@'), new(0.8037, '&'), new(0.7834, '%'), new(0.7602, 'Q'), new(0.7332, 'W'), new(0.7302, 'N'), new(0.7235, 'M'), new(0.7086, '0'), new(0.7039, 'g'), new(0.6925, 'B'), new(0.6816, '$'), new(0.6809, '#'), new(0.6759, 'D'), new(0.6714, 'R'), new(0.6631, '8'), new(0.6595, 'm'), new(0.6561, 'H'), new(0.6465, 'X'), new(0.6099, 'K'), new(0.6093, 'A'), new(0.6049, 'U'), new(0.6043, 'b'), new(0.5999, 'G'), new(0.5972, 'O'), new(0.587, 'p'), new(0.5818, 'V'), new(0.5777, '4'), new(0.5776, 'd'), new(0.565, '9'), new(0.5602, 'h'), new(0.5602, '6'), new(0.5591, 'P'), new(0.5569, 'k'), new(0.5567, 'q'), new(0.5509, 'w'), new(0.4992, 'S'), new(0.4953, 'E'), new(0.4944, '2'), new(0.4881, ']'), new(0.4833, 'a'), new(0.4703, 'y'), new(0.4693, 'j'), new(0.4686, 'x'), new(0.4667, 'Y'), new(0.4638, '5'), new(0.461, 'Z'), new(0.458, 'o'), new(0.4562, 'e'), new(0.4503, 'n'), new(0.4477, '['), new(0.4473, 'u'), new(0.442, 'l'), new(0.4385, 't'), new(0.4382, '1'), new(0.4328, '3'), new(0.4293, 'I'), new(0.4274, 'f'), new(0.4247, '}'), new(0.423, 'C'), new(0.42, '{'), new(0.4101, 'i'), new(0.4091, 'F'), new(0.4075, '|'), new(0.3993, '('), new(0.3984, '7'), new(0.396, 'J'), new(0.3921, ')'), new(0.3838, 'v'), new(0.3747, 'T'), new(0.3737, 'L'), new(0.3667, 's'), new(0.3619, '?'), new(0.3609, 'z'), new(0.3384, '/'), new(0.3294, '*'), new(0.3232, 'c'), new(0.3192, 'r'), new(0.3099, '!'), new(0.2919, '+'), new(0.2902, '<'), new(0.2852, '>'), new(0.2571, ';'), new(0.2417, '='), new(0.2183, '^'), new(0.185, ','), new(0.1559, '_'), new(0.1403, ':'), new(0.1227, '\''), new(0.0848, '-'), new(0.0829, '.'), new(0.0751, '`'), new(0, ' ')
		//new(0.85, '\u2593'), new(0.05, '\u2592'), new(0.01, '\u2591'), new(0, ' ')
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
		return buffer[x, y].z > 0 && buffer[x, y].z <= z;
	}
	
	// Returns the buffer width
	public int GetWidth()
	{
		return buffer.GetLength(0);
	}
	
	// Returns the buffer height
	public int GetHeight()
	{
		return buffer.GetLength(1);
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