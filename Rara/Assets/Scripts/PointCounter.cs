using System.Collections;
using System.Collections.Generic;
using Messages;
using TMPro;
using UniRx;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public TextMeshProUGUI CoinDisplay;
    private int _coins = 0;

    void Start()
    {
        GameMaster.Instance.CurrentAppState.Where(st => st == ActivateUIMessage.AppStateTypes.Simulation).Subscribe(_ =>
        {
            _coins = 0;
            CoinDisplay.text = _coins.ToString();
        }).AddTo(this);

        MessageBroker.Default.Receive<AddCoinMessage>().Subscribe(_ =>
        {
            _coins++;
            CoinDisplay.text = _coins.ToString();
        }).AddTo(this);
    }
}