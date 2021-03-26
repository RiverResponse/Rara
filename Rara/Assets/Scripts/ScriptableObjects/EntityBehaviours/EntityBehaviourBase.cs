using UnityEngine;

public class EntityBehaviourBase : ScriptableObject
{
    /// <summary>
    /// Indicator of what type is this behaviour
    /// </summary>
    public int Type { get; protected set; }

    /// <summary>
    /// This object will be instantiated when triggered
    /// </summary>
    public GameObject ActionIndicator;
    
    /// <summary>
    /// Icon of the Behaviour
    /// </summary>
    public Sprite Icon;

    /// <summary>
    /// Action to be executed when triggered
    /// </summary>
    public virtual void BehaviourAction()
    {
    }
}