using System.Collections;
using System.Collections.Generic;
using Messages;
using UniRx;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public Camera Camera;
    public LayerMask PresenterLayerMask;

    void Start()
    {
        Observable.EveryUpdate().Where(_ => GameMaster.Instance.CurrentAppState.Value == ActivateUIMessage.AppStateTypes.Simulation).Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => RaycastAndTrigger()).AddTo(this);
    }

    void RaycastAndTrigger()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, PresenterLayerMask))
        {
            hit.transform.parent.GetComponent<EntityPresenter>().Triggered();
        }
    }
}