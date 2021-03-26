using UnityEngine;

/// <summary>
/// Basic data for each entity
/// </summary>
[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    /// <summary>
    /// Visual representation of the Entity
    /// </summary>
    public GameObject Prefab;
    
    /// <summary>
    /// Icon of the entity
    /// </summary>
    public Sprite Icon;
}