using UnityEngine;

/// <summary>
/// <see cref="EntityBehaviourBase"/>, creates explosion when triggered
/// </summary>
[CreateAssetMenu(fileName = "ExplosionBehaviour", menuName = "ScriptableObjects/ExplosiotBehaviour", order = 1)]
public class ExplosionBehaviour : EntityBehaviourBase
{
    private bool _triggered = false;

    public ExplosionBehaviour()
    {
        Type = 0;
    }

    ///<inheritdoc cref="EntityBehaviourBase.BehaviourAction"/>
    public override void BehaviourAction(Vector3 position)
    {
        if (!_triggered)
        {
            base.BehaviourAction(position);
            _triggered = true;
            Debug.Log("Explosion triggered");
        }
    }

    public override void SetToDefault()
    {
        _triggered = false;
    }
}