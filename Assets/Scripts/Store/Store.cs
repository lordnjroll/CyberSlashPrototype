using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject StoreLayer;
    [SerializeField] private GameObject itemPrefab;
    public static Store Instance;

    public List<StoreItem> ItemList = new List<StoreItem>();
    public List<StoreItem> ItemKeeper = new List<StoreItem>();

    public List<ItemUI> itemsList = new List<ItemUI>();

    public Transform itemcontent;
    public GameObject InventoryItem;
    int ItemCount = 0;

    public static int healthpotion = 0;
    public static int bonuspotion = 0;
    public static int powerup = 0;
    public static int Skill1 = 0;
    public static int Skill2 = 0;
    private void Awake()
    {
        Instance = this;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StoreLayer.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ListItem();
        }

        
    }



    public void ListItem()
    {
        if(ItemCount < ItemList.Count)
        {
            foreach (var item in ItemList)
            {
                ItemUI obj = Instantiate(InventoryItem, itemcontent).GetComponent<ItemUI>();

                obj.data = item;

                
                obj.AddItemEvent(MinusBtnOnClick);

                obj.ItemName.text = item.itemName;
                obj.ItemIcon.sprite = item.itemicon;
                obj.LevelText.text = item.level + " / 10";
                obj.ValueTxt.text = "$" + item.value;

                itemsList.Add(obj);

                ItemCount++;
            }
        }
        
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }


    public void MinusBtnOnClick(StoreItem data)
    {
        Debug.Log(data.itemName);

        if (data.itemName.Equals("HealthPotion"))
        {
            healthpotion++;
        }

        if (data.itemName.Equals("EXPPotion"))
        {
            bonuspotion++;
        }

        if (data.itemName.Equals("PowerUp"))
        {
            powerup++;
        }

        if (data.itemName.Equals("Skill1"))
        {
            Skill1++;
        }

        if (data.itemName.Equals("Skill2"))
        {
            Skill2++;
        }
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreManager sc = new ScoreManager();

        formatter.Serialize(stream, sc);
        stream.Close();

    }

    public void Load()
    {

    }
}
