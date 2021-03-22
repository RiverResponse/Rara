using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GivePointsBehaviour", menuName = "ScriptableObjects/GivePointsBehaviour", order = 1)]
public class GivePointsBehaviour : EntityBehaviourBase
{
    public GivePointsBehaviour()
    {
        Type = 1;
    }
    
    public override void BehaviourAction()
    {
        Debug.Log("Point adding triggered");
    }
}