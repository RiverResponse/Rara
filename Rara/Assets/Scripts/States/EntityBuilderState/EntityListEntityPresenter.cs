using Messages;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EntityListEntityPresenter : MonoBehaviour
{
    public ReactiveProperty<EntityBase> MyEntity;

    public Image IconImage;
    public TextMeshProUGUI NameLabel;
    public TextMeshProUGUI DescLabel;
    public Button SelectEntityButton;
    public GameObject SelectionIndicator;

    void Start()
    {
        GameMaster.Instance.SelectedEntity.Subscribe(x => SelectionIndicator.SetActive(x != null && x == MyEntity.Value)).AddTo(this);
        SelectEntityButton.onClick.AddListener(ChooseEntity);
        BindData();
    }

    private void ChooseEntity()
    {
        MessageBroker.Default.Publish(new SelectEntityBaseMessage(MyEntity.Value));
    }


    void BindData()
    {
        IconImage.sprite = MyEntity.Value.EntityData.Value.Icon;

        MyEntity.Value.Name.Subscribe(s => NameLabel.text = s).AddTo(this);
        MyEntity.Value.Description.Subscribe(s => DescLabel.text = s).AddTo(this);
    }

    public void Init(EntityBase entity)
    {
        MyEntity.Value = entity;
    }
}