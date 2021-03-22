using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EntityBehaviourToggle : MonoBehaviour
{
    public Button AttachButton;
    public Image BehaviourImage;

    private bool _isAttached => !_entityBase.EntityBehaviourCanBeAdded(_behaviour);

    private EntityBase _entityBase;
    private EntityBehaviourBase _behaviour;

    private List<IDisposable> _temporarySubscriptions = new List<IDisposable>();

    public void Init(EntityBehaviourBase behaviour)
    {
        for (int i = 0; i < _temporarySubscriptions.Count; i++)
        {
            _temporarySubscriptions[i].Dispose();
        }

        _temporarySubscriptions.Clear();


        _behaviour = behaviour;

        BehaviourImage.sprite = _behaviour.Icon;
        AttachButton.onClick.AddListener(ButtonClicked);
    }

    public void BindEntity(EntityBase entityBase)
    {
        _entityBase = entityBase;
        _entityBase.Behaviours.ObserveCountChanged(true).Subscribe(_ => ReflectIsAttached()).AddTo(this);
    }

    private void ButtonClicked()
    {
        if (_isAttached)
        {
            _entityBase.RemoveBehavior(_behaviour);
        }
        else
        {
            _entityBase.AddBehavior(_behaviour);
        }
    }

    private void ReflectIsAttached()
    {
        BehaviourImage.color = _isAttached ? Color.green : Color.white;
    }
}