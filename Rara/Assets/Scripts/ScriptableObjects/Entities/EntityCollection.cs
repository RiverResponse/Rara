using UnityEngine;

/// <summary>
/// All entities should be part of this class
/// </summary>
[CreateAssetMenu(fileName = "EntityCollection", menuName = "ScriptableObjects/EntityCollection", order = 1)]
public class EntityCollection : ScriptableObject
{
    [Tooltip("Cube shaped entity")]
    public EntityData CubeEntity;
    
    [Tooltip("Sphere shaped entity")]
    public EntityData SphereEntity;
}