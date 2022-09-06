using System;

public static class Utils
{
    public static float Distance(float value, float min, float max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}