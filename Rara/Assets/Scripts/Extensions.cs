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
}