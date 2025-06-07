using UnityEngine;

public static class Vector2Extension
{
    public static bool IsNear(this Vector2 vector1, Vector2 vector2, float minDistance = 0.1f)
    {
        return (vector1 - vector2).magnitude <= minDistance;
    }
    /// <summary>
    ///      90
    /// 180       0
    ///     -90
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static float ToAngle(this Vector2 vector)
      => Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
}
public static class Vector3Extension
{
    public static bool IsNear(this Vector3 vector1, Vector3 vector2, float minDistance = 0.1f)
    {
        return (vector1 - vector2).magnitude <= minDistance;
    }
    public static Vector3 To(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
}