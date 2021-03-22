using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ExplosionBehaviour", menuName = "ScriptableObjects/ExplosiotBehaviour", order = 1)]
public class ExplosionBehaviour : EntityBehaviourBase
{
    public ExplosionBehaviour()
    {
        Type = 0;
    }

    public override void BehaviourAction()
    {
        Debug.Log("Explosion triggered");
    }
}