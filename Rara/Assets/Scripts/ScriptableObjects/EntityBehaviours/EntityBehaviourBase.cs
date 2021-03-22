using UnityEngine;

public class EntityBehaviourBase : ScriptableObject
{
    public int Type { get; protected set; }
    public GameObject ActionIndicator;
    public Sprite Icon;

    public virtual void BehaviourAction()
    {
    }
}