
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemUI : MonoBehaviour
{
    public StoreItem data;

    public Button ItemPrefab;
    public Image ItemIcon;
    public TMP_Text ItemName;
    public TMP_Text ValueTxt;

    public void AddItemEvent (UnityAction<StoreItem> _event)
    {
        Debug.Log("AddMinusEvent");
        ItemPrefab.onClick.AddListener(()=>_event(data));
    }
}
