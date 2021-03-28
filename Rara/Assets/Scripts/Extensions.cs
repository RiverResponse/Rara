using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Collection of Class extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Sets the layer for all child objects
    /// </summary>
    /// <param name="obj">Target object</param>
    /// <param name="layer">New layer</param>
    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
        if (obj == null)
        {
            return;
        }

        foreach (Transform trans in obj.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layer;
        }
    }

    public static Vector3 Clamp(this Vector3 target, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(target.x, min.x, max.x),
            Mathf.Clamp(target.y, min.y, max.y),
            Mathf.Clamp(target.z, min.z, max.z));
    }
}