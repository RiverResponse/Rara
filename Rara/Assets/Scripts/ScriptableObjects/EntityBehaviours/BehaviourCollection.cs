using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the behaviours should be part of this class
/// </summary>
[CreateAssetMenu(fileName = "BehaviourCollection", menuName = "ScriptableObjects/BehaviourCollection", order = 1)]
public class BehaviourCollection : ScriptableObject
{
    /// <summary>
    /// All attachable behaviours
    /// </summary>
    public List<EntityBehaviourBase> Behaviors;
}
