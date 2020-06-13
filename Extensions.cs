using OpenToolkit.Mathematics;

public static class HelperExtensions
{
    public static bool IsBetween(this float val, float lowerBound, float upperBound)
    {
        return val >= lowerBound && val <= upperBound;
    }


    public static bool IsInCornerRadius(this Vector2 val, float top, float bottom, float left, float right, float radius)
    {
        var topLeft = new Vector2(left, top);
        var topRight = new Vector2(right, top);
        var bottomLeft = new Vector2(left, bottom);
        var bottomRight = new Vector2(right, bottom);

        return Vector2.Distance(topLeft, val) <= radius
         || Vector2.Distance(topRight, val) <= radius
        || Vector2.Distance(bottomLeft, val) <= radius
        || Vector2.Distance(bottomRight, val) <= radius;

    }
}