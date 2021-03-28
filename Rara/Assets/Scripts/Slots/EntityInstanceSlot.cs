using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EntityInstanceSlot : MonoBehaviour
{
    public bool IsEmpty => _currentEntity == null;
    protected EntityPresenter _currentEntity;
    public MeshRenderer MeshRenderer;
    public Material NotHoverMaterial;
    public Material HoverMaterial;
    private BoolReactiveProperty _isHovered = new BoolReactiveProperty(false);

    void Start()
    {
        if (MeshRenderer != null)
        {
            _isHovered.Subscribe(b => MeshRenderer.sharedMaterial = b ? HoverMaterial : NotHoverMaterial).AddTo(this);
        }
    }

    protected void PlaceEntityInstance(EntityPresenter instance)
    {
        _currentEntity = instance;
        _currentEntity.transform.position = transform.position;
    }

    public void SetIsHovered(bool isHovered)
    {
        _isHovered.Value = isHovered;
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