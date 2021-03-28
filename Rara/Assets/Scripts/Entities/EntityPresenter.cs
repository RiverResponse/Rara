using System;
using System.IO.Ports;
using Messages;
using UniRx;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// Displays given <see cref="EntityBase"/> object's prefab
/// </summary>
public class EntityPresenter : MonoBehaviour
{
    /// <summary>
    /// Entity data to be displayed
    /// </summary>
    public EntityBase Data { get; private set; }

    private ReactiveProperty<EntityInstanceSlot> _currentSlot = new ReactiveProperty<EntityInstanceSlot>();

    private Collider _collider;
    private MeshRenderer _meshRenderer;
    public Material HoverMaterial;
    public Material NotHoverMaterial;
    public Material Selectedmaterial;

    private GameObject _instance;
    private BoolReactiveProperty _isHovered = new BoolReactiveProperty(false);


    public void Initialize(EntityBase data, EntityInstanceSlot slot)
    {
        Data = data;
        _currentSlot.Pairwise().Subscribe(pair =>
        {
            pair.Previous?.SetPresenter(null);
            pair.Current.SetPresenter(this);
        }).AddTo(this);

        _instance = Instantiate(Data.EntityData.Value.Prefab, transform);
        _instance.SetLayerRecursively(gameObject.layer);
        _instance.transform.localPosition = Vector3.zero;
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _collider = GetComponentInChildren<Collider>();
        _currentSlot.Value = slot;

        _isHovered.Subscribe(b => _meshRenderer.sharedMaterial = b ? HoverMaterial : NotHoverMaterial).AddTo(this);

        MessageBroker.Default.Receive<RemoveEntityMessage>().Subscribe(msg =>
            {
                if (msg.EntityBase == Data)
                {
                    Destroy(gameObject);
                }
            }
        ).AddTo(this);

        GameMaster.Instance.SelectedEntity.CombineLatest(GameMaster.Instance.CurrentAppState, (entityBase, appState) => true).Subscribe(_ =>
            _meshRenderer.sharedMaterial = GameMaster.Instance.SelectedEntity.Value == Data && GameMaster.Instance.CurrentAppState.Value == ActivateUIMessage.AppStateTypes.EntityEditor
                ? Selectedmaterial
                : NotHoverMaterial).AddTo(this);
    }

    private void OnDestroy()
    {
        _currentSlot.Value.SetPresenter(null);
    }

    public void SetCurrentSlot(EntityInstanceSlot slot)
    {
        _currentSlot.Value = slot;
    }

    public void SetIsHovered(bool isHovered)
    {
        _isHovered.Value = isHovered;
    }

    public void DragStarted()
    {
        _collider.enabled = false;
    }

    public void DragEnded()
    {
        _collider.enabled = true;
        if (_currentSlot.Value is DestroyEntityInstanceSlot)
        {
            Destroy(gameObject);
        }
    }

    public void Triggered()
    {
        Debug.Log("Triggered");
        foreach (var behaviour in Data.Behaviours)
        {
            behaviour.BehaviourAction(transform.position);
        }
    }
}