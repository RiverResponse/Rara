using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourCollection", menuName = "ScriptableObjects/BehaviourCollection", order = 1)]
public class BehaviourCollection : ScriptableObject
{
    public List<EntityBehaviourBase> Behaviors;
}
