using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject gameUILayer;
    public GameObject SettingLayer;
    public GameObject DisplayLayer;
    public GameObject VolumeLayer;
    public GameObject ClearLayer;
    public GameObject deathLayer;

    private bool menuSwitch = false;

    private hp PlayerHP;

    // Update is called once per frame
    void Update()
    {
        OpenEscMenu();
        PlayerDead();
    }

    void OpenEscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == false)
        {
            Time.timeScale = 0f;
            gameUILayer.SetActive(false);
            escMenuLayer.SetActive(true);
            menuSwitch = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("EscMenu has open");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuSwitch == true)
        {
            Time.timeScale = 1f;
            escMenuLayer.SetActive(false);
            SettingLayer.SetActive(false);
            gameUILayer.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuSwitch = false;
            Debug.Log("EscMenu has close");
        }
    }

    public void PlayerDead()
    {
        if(hp.Hp == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            escMenuLayer.SetActive(false);
            gameUILayer.SetActive(false);
            SettingLayer.SetActive(false);
            deathLayer.SetActive(true);
        }
    }

    public void ContinueBtnOnClick()
    {
        Time.timeScale = 1f;
        escMenuLayer.SetActive(false);
        SettingLayer.SetActive(false);
        gameUILayer.SetActive(true);
        menuSwitch = false;
        Cursor.visible = false;
    }

    public void MusicBtnOnClick()
    {
        escMenuLayer.SetActive(false);
        gameUILayer.SetActive(false);
        SettingLayer.SetActive(true);
    }

    public void NextStage()
    {
        SceneManager.LoadScene("Upgrade Station");
        Time.timeScale = 1f;
    }

    public void QuitBtnOnClick()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }

    public void RetryBtnOnClick()
    {
        SceneManager.LoadScene("stage 1");
        Time.timeScale = 1f;
    }

    public void DisplayBtnOnClick()
    {
        VolumeLayer.SetActive(false);
        DisplayLayer.SetActive(true);
    }

    public void VolumeBtnOnClick()
    {
        DisplayLayer.SetActive(false);
        VolumeLayer.SetActive(true);
    }
}
