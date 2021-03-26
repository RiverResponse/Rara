using UnityEngine;

/// <summary>
/// Displays given <see cref="EntityBase"/> object's prefab
/// </summary>
public class EntityPresenter : MonoBehaviour
{
    /// <summary>
    /// Entity data to be displayed
    /// </summary>
    public EntityBase Data { get; private set; }

    private GameObject _instance;


    public void Initialize(EntityBase data)
    {
        Data = data;

        _instance = Instantiate(Data.EntityData.Value.Prefab, transform);
        _instance.SetLayerRecursively(gameObject.layer);
        _instance.transform.localPosition = Vector3.zero;
    }

    public void Drag()
    {
    }
}