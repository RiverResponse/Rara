using System.Collections;
using System.Collections.Generic;
using Messages;
using UniRx;
using UnityEngine;

public class EntityInstanceDefaultSlot : EntityInstanceSlot
{
    public EntityPresenter EntityPresenterPrefab;

    void Start()
    {
        MessageBroker.Default.Receive<InstantiateEntityMessage>().Subscribe(msg => InstantiateEntity(msg.Entity)).AddTo(this);
    }

    void InstantiateEntity(EntityBase entityBase)
    {
        if (_currentEntity == null)
        {
            var currentEntity = Instantiate(EntityPresenterPrefab, transform);
            currentEntity.gameObject.SetLayerRecursively(gameObject.layer);
            currentEntity.Initialize(entityBase);
            PlaceEntityInstance(currentEntity);
        }
        else
        {
            //Can not place new entity while there is an unplaced one
        }
    }
}