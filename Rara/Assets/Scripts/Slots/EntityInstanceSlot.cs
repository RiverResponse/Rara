using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EntityInstanceSlot : MonoBehaviour
{
    public bool IsEmpty => _currentEntity == null;
    protected EntityPresenter _currentEntity;



    protected void PlaceEntityInstance(EntityPresenter instance)
    {
        _currentEntity = instance;
        _currentEntity.transform.position = transform.position;
    }

    public void SetPresenter(EntityPresenter hoveredEntityValue)
    {
        _currentEntity = hoveredEntityValue;

        if (hoveredEntityValue != null)
        {
            hoveredEntityValue.transform.position = transform.position + Vector3.up * 1.5f;
            hoveredEntityValue.SetCurrentSlot(this);
        }
    }
}