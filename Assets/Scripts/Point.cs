/// <summary>
/// Provides inner point with XY value
/// </summary>
public struct Point
{
    /// <summary>
    /// Inner X coordinate
    /// </summary>
    public int X { get; set; }

    /// Inner Y coordinate
    public int Y { get; set; }

    /// <summary>
    /// Creates point
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Point(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }
}
