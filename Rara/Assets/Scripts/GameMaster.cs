using Messages;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

/// <summary>
/// Responsible to collect and distribute commonly used properties and react to messages
/// </summary>
public class GameMaster : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the class
    /// </summary>
    public static GameMaster Instance;

    /// <summary>
    /// The currently selected <see cref="EntityBase"/>
    /// </summary>
    public ReadOnlyReactiveProperty<EntityBase> SelectedEntity;
    private readonly ReactiveProperty<EntityBase> _selectedEntity = new ReactiveProperty<EntityBase>();

    /// <summary>
    /// The currently active tool
    /// </summary>
    public ReadOnlyReactiveProperty<ActivateUIMessage.AppStateTypes> CurrentAppState;
    private readonly ReactiveProperty<ActivateUIMessage.AppStateTypes> _currentAppState = new ReactiveProperty<ActivateUIMessage.AppStateTypes>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SelectedEntity = _selectedEntity.ToReadOnlyReactiveProperty();
        CurrentAppState = _currentAppState.ToReadOnlyReactiveProperty();
        MessageBroker.Default.Receive<SelectEntityBaseMessage>().Subscribe(msg => _selectedEntity.Value = msg.Entity).AddTo(this);
        MessageBroker.Default.Receive<ActivateUIMessage>().Subscribe(msg => _currentAppState.Value = msg.AppStateType).AddTo(this);

        Observable.NextFrame().Subscribe(_ => MessageBroker.Default.Publish(new ActivateUIMessage(ActivateUIMessage.AppStateTypes.LevelEditor)));
    }

    void Start()
    {
        _selectedEntity.Value = null;
    }

    #if UNITY_EDITOR
    [Button]
    void ActivateEntityEditor()
    {
        MessageBroker.Default.Publish(new ActivateUIMessage(ActivateUIMessage.AppStateTypes.EntityEditor));
    }
    #endif
}