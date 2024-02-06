using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject gameUILayer;
    public GameObject continueLayer;
    public GameObject musicSettingLayer;
    public GameObject quitLayer;
    public GameObject deathLayer;
    public GameObject storeLayer;

    private bool menuSwitch = false;

    private hp PlayerHP;

    void Start()
    {
        
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
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == true)
        {
            Time.timeScale = 1f;
            escMenuLayer.SetActive(false);
            gameUILayer.SetActive(true);
            musicSettingLayer.SetActive(false);
            menuSwitch = false;
            Debug.Log("EscMenu has close");
        }
    }

    void GoBackEscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }

    //public void OpenStore()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        storeLayer.SetActive(true);
    //    }
    //}

    public void PlayerDead()
    {
        if(hp.Hp == 0)
        {
            deathLayer.SetActive(true);
            escMenuLayer.SetActive(false);
            gameUILayer.SetActive(false);
            musicSettingLayer.SetActive(false);
        }
    }

    public void ContinueBtnOnClick()
    {
        Time.timeScale = 1f;
        escMenuLayer.SetActive(false);
        gameUILayer.SetActive(true);
        menuSwitch = false;
        Cursor.visible = false;
        Debug.Log("Clicked");
    }

    public void MusicBtnOnClick()
    {
        musicSettingLayer.SetActive(true);
        escMenuLayer.SetActive(false);
    }

    public void QuitBtnOnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RetryBtnOnClick()
    {
        SceneManager.LoadScene("stage 1");
        Time.timeScale = 1f;
    }
}
