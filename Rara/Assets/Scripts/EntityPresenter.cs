using UnityEditorInternal;
using UnityEngine;

public class EntityPresenter : MonoBehaviour
{
    public EntityBase Data;

    private GameObject _instance;

    void Start()
    {
        _instance = Instantiate(Data.EntityData.Value.Prefab, transform);
        _instance.transform.localPosition = Vector3.zero;
    }
}