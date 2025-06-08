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
    public static Vector3 NewX(this Vector3 v, float x)
    {
        return new(x, v.y, v.z);
    }
    public static Vector3 NewY(this Vector3 v, float y)
    {
        return new(v.x, y, v.z);
    }
    public static Vector3 NewZ(this Vector3 v, float z)
    {
        return new(v.x, v.y, z);
    }
    public static Vector3 AddToX(this Vector3 v, float x)
    {
        return new(v.x + x, v.y, v.z);
    }
    public static Vector3 AddToY(this Vector3 v, float y)
    {
        return new(v.x, v.y + y, v.z);
    }
    public static Vector3 AddToZ(this Vector3 v, float z)
    {
        return new(v.x, v.y, v.z + z);
    }
}