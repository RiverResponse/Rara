using UnityEngine;

/// <summary>
/// Displays given <see cref="EntityBase"/> object's prefab
/// </summary>
public class EntityPresenter : MonoBehaviour
{
    /// <summary>
    /// Entity data to be displayed
    /// </summary>
    public EntityBase Data;

    private GameObject _instance;

    void Start()
    {
        if (Data != null)
        {
            _instance = Instantiate(Data.EntityData.Value.Prefab, transform);
            _instance.SetLayerRecursively(gameObject.layer);
            _instance.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Please add EntityBase on the same frame as instantiated");
        }
    }
}