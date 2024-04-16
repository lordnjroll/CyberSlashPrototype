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
    [SerializeField] private TMP_Text Tipstxt;
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
        if (ItemCount < ItemList.Count)
        {
            foreach (var item in ItemList)
            {
                ItemUI obj = Instantiate(InventoryItem, itemcontent).GetComponent<ItemUI>();

                obj.data = item;

                obj.AddItemEvent(AdItemOnClick);

                obj.ItemName.text = item.itemName;
                obj.ItemIcon.sprite = item.itemicon;
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

    public void AdItemOnClick(StoreItem data)
    {
        if (data.itemName.Equals("HealthPotion") && ScoreManager.score >= data.value)
        {
            ScoreManager.score -= data.value;
            healthpotion++;
        }
        else if (data.itemName.Equals("HealthPotion") && ScoreManager.score < data.value)
        {
            Tipstxt.text = "You don't have enough score to buy this item.";
            StartCoroutine(ClearText());
        }

        if (data.itemName.Equals("EXPPotion") && ScoreManager.score >= data.value)
        {
            ScoreManager.score -= data.value;
            bonuspotion++;
        }
        else if (data.itemName.Equals("EXPPotion") && ScoreManager.score < data.value)
        {
            Tipstxt.text = "You don't have enough score to buy this item.";
            StartCoroutine(ClearText());
        }

        if (data.itemName.Equals("PowerUp") && ScoreManager.score >= data.value)
        {
            ScoreManager.score -= data.value;
            powerup++;
        }
        else if (data.itemName.Equals("PowerUp") && ScoreManager.score < data.value)
        {
            Tipstxt.text = "You don't have enough score to buy this item.";
            StartCoroutine(ClearText());
        }

        if (data.itemName.Equals("Skill1") && ScoreManager.score >= data.value)
        {
            ScoreManager.score -= data.value;
            Skill1++;
        }
        else if (data.itemName.Equals("Skill1") && ScoreManager.score < data.value)
        {
            Tipstxt.text = "You don't have enough score to buy this item.";
            StartCoroutine(ClearText());
        }

        if (data.itemName.Equals("Skill2") && ScoreManager.score >= data.value)
        {
            ScoreManager.score -= data.value;
            Skill2++;
        }
        else if (data.itemName.Equals("Skill2") && ScoreManager.score < data.value)
        {
            Tipstxt.text = "You don't have enough score to buy this item.";
            StartCoroutine(ClearText());
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

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        Tipstxt.text = " ";
    }
}
