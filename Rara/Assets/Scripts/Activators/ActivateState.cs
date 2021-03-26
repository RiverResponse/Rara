using Messages;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActivateState : MonoBehaviour
{
    public ActivateUIMessage.AppStateTypes NextState;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => MessageBroker.Default.Publish(new ActivateUIMessage(NextState)));
    }
}