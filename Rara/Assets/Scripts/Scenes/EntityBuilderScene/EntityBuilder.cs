using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class should show all the properties inside the entity builder scene
/// </summary>
public class EntityBuilder : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("")]
    public EntityCollection EntityCollection;

    [Header("Data")]
    [Tooltip("Button to show the entity types")]
    public Button CreateEntityButton;

    [Tooltip("Button to create new Cube entity and select it")]
    public Button CreateCubeEntityButton;

    [Tooltip("Button to create new Sphere entity and select it")]
    public Button CreateSphereEntityButton;

    [Tooltip("Root object for the possible entity types")]
    public GameObject EntityTypes;

    [Tooltip("Entity inspector's root")]
    public GameObject EntityInspector;

    [Tooltip("Entity presenter in the list")]
    public EntityListEntityPresenter EntityListEntityPresenterPrefab;

    [Tooltip("Root object for the list elements")]
    public RectTransform InstanceRoot;


    private ReactiveCollection<EntityBase> _entities;
    private List<EntityListEntityPresenter> _entityButtons;
    private ReactiveProperty<EntityBase> _currentEntity;

    private void Awake()
    {
        _entities = new ReactiveCollection<EntityBase>();
        _currentEntity = new ReactiveProperty<EntityBase>();
        _entityButtons = new List<EntityListEntityPresenter>();
    }

    void Start()
    {
        CreateEntityButton.onClick.AddListener(AddNewEntity);
        CreateCubeEntityButton.onClick.AddListener(CreateCubeEntity);
        CreateSphereEntityButton.onClick.AddListener(CreateSphereEntity);

        _currentEntity.Where(x => x != null).Subscribe(_ => ReactToEntityChanged()).AddTo(this);
        _currentEntity.Where(x => x == null).Subscribe(_ => ClearUI()).AddTo(this);

        _entities.ObserveAdd().Subscribe(ReactToEntityAdded).AddTo(this);
        _entities.ObserveRemove().Subscribe(ReactToEntityRemoved).AddTo(this);
        MessageBroker.Default.Receive<ChooseEntityMessage>().Subscribe(msg => ReactToEntitySelected(msg.Entity)).AddTo(this);
        MessageBroker.Default.Receive<RemoveEntity>().Subscribe(msg => _entities.Remove(msg.EntityBase)).AddTo(this);
    }

    private void ReactToEntitySelected(EntityBase objEntity)
    {
        _currentEntity.Value = objEntity;
        EntityTypes.SetActive(false);
    }

    private void ReactToEntityAdded(CollectionAddEvent<EntityBase> e)
    {
        var instance = Instantiate(EntityListEntityPresenterPrefab, InstanceRoot);
        instance.Init(e.Value);
        _entityButtons.Add(instance);
    }

    private void ReactToEntityRemoved(CollectionRemoveEvent<EntityBase> e)
    {
        var instance = _entityButtons.First(b => b.MyEntity.Value == e.Value);
        if (instance != null)
        {
            Destroy(instance.gameObject);
            MessageBroker.Default.Publish(new ChooseEntityMessage(null));
        }
    }

    private void AddNewEntity()
    {
        EntityTypes.SetActive(true);
    }

    [Button]
    private void CreateCubeEntity()
    {
        var entity = new EntityBase();
        entity.EntityData.Value = EntityCollection.CubeEntity;
        _entities.Add(entity);
        MessageBroker.Default.Publish(new ChooseEntityMessage(entity));
    }

    [Button]
    private void CreateSphereEntity()
    {
        var entity = new EntityBase();
        entity.EntityData.Value = EntityCollection.SphereEntity;
        _entities.Add(entity);
        MessageBroker.Default.Publish(new ChooseEntityMessage(entity));
    }

    private void ReactToEntityChanged()
    {
        // EntityInspector.SetActive(true);
    }

    private void ClearUI()
    {
        // EntityInspector.SetActive(false);
    }
}