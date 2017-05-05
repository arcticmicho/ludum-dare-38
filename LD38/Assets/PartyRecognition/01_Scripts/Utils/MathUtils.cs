using UnityEngine;
using System.Collections;

public class MathUtils
{
    /// <summary>
    /// Computes the Squared Euclidean Distance between two points in 2D
    /// </summary>
    public static float SqrEuclideanDistance(Vector2 a, Vector2 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
    }

    /// <summary>
    /// Computes the Euclidean Distance between two points in 2D
    /// </summary>
    public static float EuclideanDistance(Vector2 a, Vector2 b)
    {
        return Mathf.Sqrt(SqrEuclideanDistance(a, b));
    }
}
