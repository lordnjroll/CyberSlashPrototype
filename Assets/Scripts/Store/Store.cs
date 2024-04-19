using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject GameUILayer;
    [SerializeField] private GameObject StoreLayer;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TMP_Text Tipstxt;

    private UIManager uiManager;
    public static Store Instance;

    public List<StoreItem> ItemList = new List<StoreItem>();
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
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StoreLayer.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    ListItem();
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("stage 2");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameUILayer.SetActive(false);
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
        Cursor.lockState = CursorLockMode.Locked;
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

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        Tipstxt.text = " ";
    }

    public void CloseBtnOnClick()
    {
        StoreLayer.SetActive(false);
        GameUILayer.SetActive(true);
    }
}
