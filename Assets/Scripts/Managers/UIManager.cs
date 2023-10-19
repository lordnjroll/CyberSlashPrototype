using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject gameUILayer;
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
            Time.timeScale = 0f;
            escMenuLayer.SetActive(true);
            gameUILayer.SetActive(false);
            menuSwitch = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("EscMenu has open");
        }else if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == true)
        {
            Time.timeScale = 1f;
            escMenuLayer.SetActive(false);
            gameUILayer.SetActive(true);
            menuSwitch = false;
            Debug.Log("EscMenu has close");
        }
    }

    public void ContinueBtnOnClick()
    {
        Time.timeScale = 0f;
        musicLayer.SetActive(false);
        quitLayer.SetActive(false);
        Debug.Log("Clicked");
    }
}
