﻿using UnityEngine;

public static class Vector2Extension
{
    public static bool IsNear(this Vector2 vector1, Vector2 vector2, float minDistance = 0.1f)
    {
        return (vector1 - vector2).magnitude <= minDistance;
    }
    //     90
    // 180     0
    //    -90
    public static float ToAngle(this Vector2 vector)
      => Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

    public static Vector2 To(this Vector2 from, Vector2 to)
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
