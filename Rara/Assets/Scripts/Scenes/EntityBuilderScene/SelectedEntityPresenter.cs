using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SelectedEntityPresenter : UIBase
{
    private GameObject _entityInstance;

    void Start()
    {
        GameMaster.Instance.SelectedEntity.Subscribe(SelectedEntityChanged).AddTo(this);
    }

    void SelectedEntityChanged(EntityBase entityBase)
    {
        if (_entityInstance != null)
        {
            Destroy(_entityInstance);
        }

        if (entityBase != null)
        {
            _entityInstance = Instantiate(entityBase.EntityData.Value.Prefab);
            _entityInstance.transform.localPosition = Vector3.zero;
            _entityInstance.SetLayerRecursively(gameObject.layer);
        }
    }
}