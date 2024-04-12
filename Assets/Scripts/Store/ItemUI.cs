
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemUI : MonoBehaviour
{
    public StoreItem data;

    public Image ItemIcon;
    public TMP_Text ItemName;
    public Button PlusBtn;
    public Button MinusBtn;
    public TMP_Text LevelText;
    public TMP_Text ValueTxt;

    public void AddMinusEvent (UnityAction<StoreItem> _event)
    {
        Debug.Log("AddMinusEvent");
        MinusBtn.onClick.AddListener(()=>_event(data));
    }

    public void AddPlusEvent (UnityAction<StoreItem> _event)
    {
        Debug.Log("AddPlusEvent");
        PlusBtn.onClick.AddListener(() => _event(data));
    }
}
