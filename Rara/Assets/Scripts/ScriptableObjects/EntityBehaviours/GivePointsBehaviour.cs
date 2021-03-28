using UnityEngine;

/// <summary>
/// <see cref="EntityBehaviourBase"/>, gives point when triggered
/// </summary>
[CreateAssetMenu(fileName = "GivePointsBehaviour", menuName = "ScriptableObjects/GivePointsBehaviour", order = 1)]
public class GivePointsBehaviour : EntityBehaviourBase
{
    public GivePointsBehaviour()
    {
        Type = 1;
    }

    ///<inheritdoc cref="EntityBehaviourBase.BehaviourAction"/>
    public override void BehaviourAction(Vector3 position)
    {
        base.BehaviourAction(position);
        Debug.Log("Point adding triggered");
    }
}