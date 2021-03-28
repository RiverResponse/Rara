using UnityEngine;

/// <summary>
/// <see cref="EntityBehaviourBase"/>, creates explosion when triggered
/// </summary>
[CreateAssetMenu(fileName = "ExplosionBehaviour", menuName = "ScriptableObjects/ExplosiotBehaviour", order = 1)]
public class ExplosionBehaviour : EntityBehaviourBase
{
    public ExplosionBehaviour()
    {
        Type = 0;
    }

    ///<inheritdoc cref="EntityBehaviourBase.BehaviourAction"/>
    public override void BehaviourAction(Vector3 position)
    {
        base.BehaviourAction(position);
        Debug.Log("Explosion triggered");
    }
}