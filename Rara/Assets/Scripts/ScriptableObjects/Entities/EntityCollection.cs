using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityCollection", menuName = "ScriptableObjects/EntityCollection", order = 1)]
public class EntityCollection : ScriptableObject
{
    public EntityData CubeEntity;
    public EntityData SphereEntity;
}