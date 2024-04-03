using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject StoreLayer;
    [SerializeField] private Transform container;
    [SerializeField] private Transform upgradeItem;

    private IShopCustomer ishop;

    private void Update()
    {
        StoreFunction();
    }


    void StoreFunction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("opened layer");
            //container = transform.Find("container");
            //upgradeItem = container.Find("upgradeItem");
            StoreLayer.SetActive(true);
            CreateItemButton(StoreItem.ItemType.Potion1, StoreItem.GetSprite(StoreItem.ItemType.Potion1), "Postion 1", StoreItem.GetCost(StoreItem.ItemType.Potion1), 0);
            CreateItemButton(StoreItem.ItemType.Potion2, StoreItem.GetSprite(StoreItem.ItemType.Potion2), "Postion 2", StoreItem.GetCost(StoreItem.ItemType.Potion2), 1);
            CreateItemButton(StoreItem.ItemType.PowerUp, StoreItem.GetSprite(StoreItem.ItemType.PowerUp), "PowerUp", StoreItem.GetCost(StoreItem.ItemType.PowerUp), 2);
            CreateItemButton(StoreItem.ItemType.SpeedUp, StoreItem.GetSprite(StoreItem.ItemType.SpeedUp), "SpeedUp", StoreItem.GetCost(StoreItem.ItemType.SpeedUp), 3);
            CreateItemButton(StoreItem.ItemType.Shield, StoreItem.GetSprite(StoreItem.ItemType.Shield), "Shield", StoreItem.GetCost(StoreItem.ItemType.Shield), 4);
        }
    }

    void CreateItemButton(StoreItem.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform upgradeItemTransform = Instantiate(upgradeItem, container);
        RectTransform upgradeItemRectTransform = upgradeItemTransform.GetComponent<RectTransform>();

        float upgradeItemHeight = 30f;
        upgradeItemRectTransform.anchoredPosition = new Vector2(0, -upgradeItemHeight * positionIndex);

        upgradeItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        upgradeItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        upgradeItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        upgradeItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            TryBuyItem(itemType);
        };
    }

    void TryBuyItem(StoreItem.ItemType itemType)
    {
        if (ishop.TrySpendGoldAmount(StoreItem.GetCost(itemType)))
        {
            ishop.BoughtItem(itemType);
        }
        
    }

}
