using System;
using Messages;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using System.Security.Cryptography;
using UnityEditor;

#endif

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

    public Object EntityBuilderScene;
    public Object MainScene;

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

        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene(EntityBuilderScene.name, LoadSceneMode.Additive);
        }

        SelectedEntity = _selectedEntity.ToReadOnlyReactiveProperty();
        CurrentAppState = _currentAppState.ToReadOnlyReactiveProperty();
        MessageBroker.Default.Receive<SelectEntityBaseMessage>().Subscribe(msg => _selectedEntity.Value = msg.Entity).AddTo(this);
        MessageBroker.Default.Receive<ActivateUIMessage>().Subscribe(msg => _currentAppState.Value = msg.AppStateType).AddTo(this);

        Observable.NextFrame(FrameCountType.FixedUpdate).Subscribe(_ => MessageBroker.Default.Publish(new ActivateUIMessage(ActivateUIMessage.AppStateTypes.EntityEditor)));

        CurrentAppState.Subscribe(scene =>
        {
            switch (scene)
            {
                case ActivateUIMessage.AppStateTypes.EntityEditor:
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(EntityBuilderScene.name));
                    break;
                case ActivateUIMessage.AppStateTypes.LevelEditor:
                case ActivateUIMessage.AppStateTypes.Simulation:
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(MainScene.name));
                    break;
            }
        }).AddTo(this);
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

#if UNITY_EDITOR
[CustomEditor(typeof(GameMaster))]
public class E_GameMaster : Editor
{
    private GameMaster instance;

    private void Awake()
    {
        instance = (GameMaster) target;
    }

    public override void OnInspectorGUI()
    {
        Object mainScene = EditorGUILayout.ObjectField("MainScene", instance.MainScene, typeof(Object), false);
        if (CheckPath(mainScene))
        {
            instance.MainScene = mainScene;
        }

        Object entityBuilderScene = EditorGUILayout.ObjectField("EntityBuilderScene", instance.EntityBuilderScene, typeof(Object), false);
        if (CheckPath(entityBuilderScene))
        {
            instance.EntityBuilderScene = entityBuilderScene;
        }
    }

    private bool CheckPath(Object obj)
    {
        if (obj != null && AssetDatabase.GetAssetPath(obj).EndsWith(".unity"))
        {
            return true;
        }
        else
        {
            Debug.LogError("Please only add Scene asset");
            return false;
        }
    }
}

#endif