using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Store : MonoBehaviour
{
    private StoreItem storeitem;
    [SerializeField] private GameObject StoreLayer;
    [SerializeField] private GameObject itemPrefab;
    public static Store Instance;

    public List<StoreItem> ItemList = new List<StoreItem>();
    public List<StoreItem> ItemKeeper = new List<StoreItem>();

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
            
        }
        itemPrefab.GetComponent<Button>().onClick.AddListener(PlusBtnOnClick);

        ListItem();
    }



    public void ListItem()
    {
        if(ItemCount < ItemList.Count)
        {
            foreach (var item in ItemList)
            {
                GameObject obj = Instantiate(InventoryItem, itemcontent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemlevel = obj.transform.Find("Leveltxt").GetComponent<TMP_Text>();
                var itemvalue = obj.transform.Find("Valuetxt").GetComponent<TMP_Text>();

                Button plusbtn = obj.transform.Find("Plusbtn").GetComponent<Button>();
                Button minusbtn = obj.transform.Find("Minusbtn").GetComponent<Button>();
                plusbtn.onClick.AddListener(PlusBtnOnClick);
                minusbtn.onClick.AddListener(MinusBtnOnClick);

                itemName.text = item.itemName;
                itemIcon.sprite = item.itemicon;
                itemlevel.text = item.level + " / 10";
                itemvalue.text = "$" + item.value;

                ItemCount++;
            }
        }
        
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void PlusBtnOnClick()
    {
        if (ScoreManager.score > storeitem.value)
        {
            ScoreManager.score = ScoreManager.score - storeitem.value;
            storeitem.level++;
        }
    }

    public void MinusBtnOnClick()
    {
        if (storeitem.level > 1)
        {
            ScoreManager.score = ScoreManager.score + storeitem.value;
            storeitem.level--;
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

    void BuyHealthPotion()
    {
        
    }

    void BuyBonusPotion()
    {

    }

    void BuyPowerUP()
    {

    }

    void BuySkill1()
    {
        
    }

    void BuySkill2()
    {

    }
}
