using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EntityDragger : MonoBehaviour
{
    public Camera SceneCamera;

    public LayerMask EntityPresenterLayer;
    public LayerMask EntitySlotLayer;

    private readonly ReactiveProperty<EntityPresenter> _hoveredEntity = new ReactiveProperty<EntityPresenter>();
    private readonly ReactiveProperty<EntityInstanceSlot> _entityInstanceSlot = new ReactiveProperty<EntityInstanceSlot>();
    private readonly BoolReactiveProperty _isDragged = new BoolReactiveProperty(false);

    private Plane _groundPlane;

    private Ray _mousePointRay => SceneCamera.ScreenPointToRay(Input.mousePosition);

    void Start()
    {
        _groundPlane = new Plane(Vector3.up, Vector3.zero);


        var editorObservable = Observable.EveryUpdate().Where(x => GameMaster.Instance.CurrentAppState.Value == ActivateUIMessage.AppStateTypes.LevelEditor);

        editorObservable.Where(_ => !_isDragged.Value).Subscribe(x => CheckForGrabbable()).AddTo(this);
        editorObservable.Where(_ => _isDragged.Value).Subscribe(x => CheckForSlot()).AddTo(this);

        editorObservable.Where(_ => _hoveredEntity.Value != null).Where(_ => Input.GetMouseButtonDown(0)).Subscribe(x =>
        {
            _hoveredEntity.Value.DragStarted();
            _isDragged.Value = true;
        }).AddTo(this);
        editorObservable.Where(_ => _hoveredEntity.Value != null).Where(_ => Input.GetMouseButtonUp(0)).Subscribe(x =>
        {
            _hoveredEntity.Value.DragEnded();
            _entityInstanceSlot.Value = null;
            _isDragged.Value = false;
        }).AddTo(this);

        _hoveredEntity.Pairwise().Subscribe(pair =>
        {
            pair.Previous?.SetIsHovered(false);
            pair.Current?.SetIsHovered(true);
        }).AddTo(this);

        _entityInstanceSlot.Pairwise().Subscribe(pair =>
        {
            pair.Previous?.SetIsHovered(false);
            pair.Current?.SetIsHovered(true);
            pair.Current?.SetPresenter(_hoveredEntity.Value);
        }).AddTo(this);

        _isDragged.Subscribe(HandleIsDraggedChanged).AddTo(this);
    }

    void CheckForGrabbable()
    {
        Ray ray = _mousePointRay;
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, EntityPresenterLayer))
        {
            _hoveredEntity.Value = hit.transform.parent.GetComponent<EntityPresenter>();
        }
        else
        {
            _hoveredEntity.Value = null;
        }
    }

    public Vector3 point = Vector3.back;

    /// <summary>
    /// Gets the closest available slot
    /// </summary>
    void CheckForSlot()
    {
        var ray = _mousePointRay;
        if (_groundPlane.Raycast(ray, out float enter))
        {
            point = ray.GetPoint(enter);
            point = point.Clamp(new Vector3(-2.5f, 0, -2.5f), new Vector3(2.5f, 0, 2.5f));
            point.y = 3f;

            Ray downRay = new Ray(point, Vector3.down);
            if (Physics.Raycast(downRay, out RaycastHit hit, float.PositiveInfinity, EntitySlotLayer))
            {
                var slot = hit.transform.GetComponent<EntityInstanceSlot>();
                if (slot.IsEmpty)
                {
                    _entityInstanceSlot.Value = slot;
                }
            }

            point.y = 0f;
        }
    }


    void HandleIsDraggedChanged(bool value)
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 0.2f);
    }
}