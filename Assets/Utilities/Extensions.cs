using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

static class FloatExtension
{
    public static bool isBetween(this float value, float one, float two)
    {
        if (value >= one && value <= two) return true;
        return false;
    }
    
    public static bool isBetween(this float value, float two)
    {
        if (value <= two) return true;
        return false;
    }
}

static class Vector3Extension
{
    public static float DistanceToClosestObject(this Vector3 thisPoint, Vector3[] allPoints)
    {
        float distance = 1000;
        foreach (Vector3 point in allPoints)
        {
            float thisDistance = new Vector2(thisPoint.x - point.x, thisPoint.z - point.z).magnitude - point.y - thisPoint.y;
            if (thisDistance < distance) distance = thisDistance;
        }
        return distance;
    }
}