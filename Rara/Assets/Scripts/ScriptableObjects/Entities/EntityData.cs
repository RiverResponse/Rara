using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Icon;
}