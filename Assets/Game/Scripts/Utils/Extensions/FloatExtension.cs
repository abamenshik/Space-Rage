using UnityEngine;

public static class FloatExtension
{
    /// <summary>
    /// Return normalized Vector2.
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(this float angle)
     => new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));


    /// <summary>
    /// Maps value from original range to new range
    /// </summary>
    /// <param name="value">value to map</param>
    /// <param name="min1">original range min</param>
    /// <param name="max1">original range max</param>
    /// <param name="min2">new range min</param>
    /// <param name="max2">new range max</param>
    /// <returns>value in new range</returns>
    public static float MapRange(this float value, float min1, float max1, float min2, float max2)
    {
        var lerp = Mathf.InverseLerp(min1, max1, value);
        return Mathf.Lerp(min2, max2, lerp);
    }
}