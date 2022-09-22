using System;

public static class Utils
{
    public static float Distance(float value, float min, float max) => Math.Min(Math.Max(value, min), max);

    public static string TimeFormat(int time) => time < 10 ? $"0{time}" : $"{time}";
}