using UnityEngine;

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
    public static Vector3 XNew(this Vector3 v, float x)
    {
        return new(x, v.y, v.z);
    }
    public static Vector3 YNew(this Vector3 v, float y)
    {
        return new(v.x, y, v.z);
    }
    public static Vector3 ZNew(this Vector3 v, float z)
    {
        return new(v.x, v.y, z);
    }
    public static Vector3 XAdd(this Vector3 v, float x)
    {
        return new(v.x + x, v.y, v.z);
    }
    public static Vector3 YAdd(this Vector3 v, float y)
    {
        return new(v.x, v.y + y, v.z);
    }
    public static Vector3 ZAdd(this Vector3 v, float z)
    {
        return new(v.x, v.y, v.z + z);
    }
}