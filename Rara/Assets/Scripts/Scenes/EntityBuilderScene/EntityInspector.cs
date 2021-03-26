using System;
using System.Collections.Generic;
using Messages;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible to show and set parameters for the selected entity
/// </summary>
public class EntityInspector : UIBase
{
    public EntityBehaviourToggle EntityBehaviourTogglePrefab;
    public RectTransform EntityBehaviourToggleRoot;
    public BehaviourCollection BehaviourCollection;

    private ReactiveProperty<EntityBase> _selected = new ReactiveProperty<EntityBase>();

    public Image IconImage;

    public TMP_InputField NameInputField;
    public TMP_InputField DescInputField;
    public Button CreateEntityButton;
    public Button DeleteEntityButton;

    private List<IDisposable> _temporarySubscriptions;
    private EntityPresenter _selectedEntityInstance;

    private List<EntityBehaviourToggle> _entityBehaviourToggles = new List<EntityBehaviourToggle>();
    // private BoolReactiveProperty _isEnabled = new BoolReactiveProperty(true);
    // private BoolReactiveProperty _isActive = new BoolReactiveProperty();

    protected override void Awake()
    {
        base.Awake();
        _temporarySubscriptions = new List<IDisposable>();

        _selected.Subscribe(ReactToEntityChanged).AddTo(this);

        CreateEntityButton.onClick.AddListener(CreateInstance);
        DeleteEntityButton.onClick.AddListener(DeleteEntity);

        GameMaster.Instance.SelectedEntity.Subscribe(x => _selected.Value = x).AddTo(this);

        // _isActive.Subscribe(b => { gameObject.SetActive(b); }).AddTo(this);
        // _isEnabled.CombineLatest(_selected, (isEnabled, selected) => isEnabled && selected != null).Subscribe(b => _isActive.Value = b).AddTo(this);
        //
        // MessageBroker.Default.Receive<ActivateUIMessage>().Subscribe(msg => _isEnabled.Value = msg.SceneType == ActivateUIMessage.SceneTypes.EntityEditor).AddTo(this);
        
        for (int i = 0; i < BehaviourCollection.Behaviors.Count; i++)
        {
            var toggle = Instantiate(EntityBehaviourTogglePrefab, EntityBehaviourToggleRoot);
            toggle.Init(BehaviourCollection.Behaviors[i]);
            _entityBehaviourToggles.Add(toggle);
        }
    }

    private void ReactToEntityChanged(EntityBase entityBase)
    {
        if (_selectedEntityInstance != null)
        {
            Destroy(_selectedEntityInstance.gameObject);
        }

        if (entityBase != null)
        {
            BindData();

            foreach (var t in _entityBehaviourToggles)
            {
                t.BindEntity(entityBase);
            }
        }
    }

    private void DeleteEntity()
    {
        MessageBroker.Default.Publish(new RemoveEntityMessage(_selected.Value));
    }

    private void CreateInstance()
    {
        MessageBroker.Default.Publish(new InstantiateEntityMessage(_selected.Value));
        MessageBroker.Default.Publish(new ActivateUIMessage(ActivateUIMessage.AppStateTypes.LevelEditor));
    }

    void BindData()
    {
        foreach (var s in _temporarySubscriptions)
        {
            s.Dispose();
        }

        _temporarySubscriptions.Clear();

        if (_selected.Value == null)
        {
            return;
        }

        _selected.Value.EntityData.Subscribe(d => IconImage.sprite = d.Icon).AddTo(_temporarySubscriptions);
        _selected.Value.Name.Subscribe(s => NameInputField.text = s).AddTo(_temporarySubscriptions);
        _selected.Value.Description.Subscribe(s => DescInputField.text = s).AddTo(_temporarySubscriptions);

        NameInputField.onValueChanged.AddListener(s => _selected.Value.Name.Value = s);
        DescInputField.onValueChanged.AddListener(s => _selected.Value.Description.Value = s);
    }
}