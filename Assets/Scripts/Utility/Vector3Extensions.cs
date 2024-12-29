using UnityEngine;

public static class Vector3Extensions
{
    public static float GetSqrDistance(this Vector3 start, Vector3 end)
    {
        return (start - end).sqrMagnitude;
    }

    public static bool IsEnoughClose(this Vector3 start, Vector3 end, float distance)
    {
        return start.GetSqrDistance(end) <= distance * distance;
    }
}
