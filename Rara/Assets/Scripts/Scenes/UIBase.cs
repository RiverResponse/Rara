using Messages;
using UniRx;
using UnityEngine;

/// <summary>
/// Base class for UI panels.
/// </summary>
public class UIBase : MonoBehaviour
{
    [Tooltip("Does IsActive state depends on selected object")]
    public bool DependingOnSelection;

    [Tooltip("When should be the object activated")]
    public ActivateUIMessage.AppStateTypes activeAppStateType;

    private BoolReactiveProperty _isEnabled = new BoolReactiveProperty(false);
    private BoolReactiveProperty _isActive = new BoolReactiveProperty();


    protected virtual void Awake()
    {
        GameMaster.Instance.CurrentAppState.Subscribe(x => _isEnabled.Value = x == activeAppStateType).AddTo(this);

        if (DependingOnSelection)
        {
            _isEnabled.CombineLatest(GameMaster.Instance.SelectedEntity, (isEnabled, selected) =>
                    isEnabled && selected != null)
                .Subscribe(b =>
                    _isActive.Value = b
                ).AddTo(this);
        }
        else
        {
            _isActive = _isEnabled;
        }


        _isActive.Subscribe(b => { gameObject.SetActive(b); }).AddTo(this);
    }
}