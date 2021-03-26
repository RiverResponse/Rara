using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInstanceSlot : MonoBehaviour
{
   protected EntityPresenter _currentEntity;

   protected void PlaceEntityInstance(EntityPresenter instance)
   {
      _currentEntity = instance;
      _currentEntity.transform.position = transform.position;
   }
}
