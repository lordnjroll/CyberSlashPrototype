using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject continueLayer;
    public GameObject musicLayer;
    public GameObject quitLayer;
    private bool menuSwitch = false;

    void Start()
    {
        escMenuLayer.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        OpenEscMenu();
    }

    void OpenEscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == false)
        {
            escMenuLayer.SetActive(true);
            menuSwitch = true;
            Debug.Log("EscMenu has open");
        }else if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == true)
        {
            escMenuLayer.SetActive(false);
            menuSwitch = false;
            Debug.Log("EscMenu has close");
        }
    }
}
