using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible to show and set parameters for the selected entity
/// </summary>
public class EntityInspector : MonoBehaviour
{
    public Transform EntityInstanceRoot;
    public EntityPresenter EntityPresenterPrefab;
    public EntityBehaviourToggle EntityBehaviourTogglePrefab;
    public RectTransform EntityBehaviourToggleRoot;
    public BehaviourCollection BehaviourCollection;

    public ReactiveProperty<EntityBase> MyEntity;

    public Image IconImage;

    public TMP_InputField NameInputField;
    public TMP_InputField DescInputField;
    public Button CreateEntityButton;
    public Button DeleteEntityButton;

    private List<IDisposable> _temporarySubscriptions;
    private EntityPresenter _selectedEntityInstance;
    private List<EntityBehaviourToggle> _entityBehaviourToggles = new List<EntityBehaviourToggle>();

    private void Awake()
    {
        _temporarySubscriptions = new List<IDisposable>();
    }

    void Start()
    {
        MyEntity.Subscribe(ReactToEntityChanged).AddTo(this);

        CreateEntityButton.onClick.AddListener(CreateInstance);
        DeleteEntityButton.onClick.AddListener(DeleteEntity);

        MessageBroker.Default.Receive<ChooseEntityMessage>().Subscribe(msg => MyEntity.Value = msg.Entity).AddTo(this);
        
        for (int i = 0; i < BehaviourCollection.Behaviors.Count; i++)
        {
            var toggle = Instantiate(EntityBehaviourTogglePrefab, EntityBehaviourToggleRoot);
            toggle.Init(BehaviourCollection.Behaviors[i]);
            _entityBehaviourToggles.Add(toggle);
        }
    }

    private void ReactToEntityChanged(EntityBase entityBase)
    {
        BindData();

        if (_selectedEntityInstance != null)
        {
            Destroy(_selectedEntityInstance.gameObject);
        }

        if (entityBase != null)
        {
            _selectedEntityInstance = Instantiate(EntityPresenterPrefab, EntityInstanceRoot);
            _selectedEntityInstance.transform.localPosition = Vector3.zero;
            _selectedEntityInstance.Data = entityBase;

            foreach (var t in _entityBehaviourToggles)
            {
                t.BindEntity(entityBase);
            }
        }
    }

    private void DeleteEntity()
    {
        MessageBroker.Default.Publish(new RemoveEntity(MyEntity.Value));
    }

    private void CreateInstance()
    {
        //TODO:
    }


    void BindData()
    {
        foreach (var s in _temporarySubscriptions)
        {
            s.Dispose();
        }

        _temporarySubscriptions.Clear();

        if (MyEntity.Value == null)
        {
            return;
        }

        //This was not working a year ago. If not than uncomment the following
        // _temporarySubscriptions.Add(MyEntity.Value.Name.Subscribe(s => NameLabel.text = s));
        // _temporarySubscriptions.Add(MyEntity.Value.Description.Subscribe(s => DescLabel.text = s));
        // _temporarySubscriptions.Add(MyEntity.Value.EntityData.Subscribe(d => IconImage.sprite = d.Icon));

        MyEntity.Value.EntityData.Subscribe(d => IconImage.sprite = d.Icon).AddTo(_temporarySubscriptions);
        MyEntity.Value.Name.Subscribe(s => NameInputField.text = s).AddTo(_temporarySubscriptions);
        MyEntity.Value.Description.Subscribe(s => DescInputField.text = s).AddTo(_temporarySubscriptions);

        NameInputField.onValueChanged.AddListener(s => MyEntity.Value.Name.Value = s);
        DescInputField.onValueChanged.AddListener(s => MyEntity.Value.Description.Value = s);
    }
}