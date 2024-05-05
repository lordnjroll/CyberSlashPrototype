using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject escMenuLayer;
    public GameObject gameUILayer;
    public GameObject SettingLayer;
    public GameObject DisplayLayer;
    public GameObject VolumeLayer;
    public GameObject ClearLayer;
    public GameObject deathLayer;
    public GameObject Player;

    private bool menuSwitch = false;

    private hp PlayerHP;
    public AudioSource gameover;
    public AudioMixerSnapshot whenPaused, whenStarted;
    public List<Transform> respawnPoint = new List<Transform>();



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
            SettingLayer.SetActive(false);
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
        if(hp.Hp <= 0)
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
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SettingBtnOnClick()
    {
        escMenuLayer.SetActive(false);
        gameUILayer.SetActive(false);
        SettingLayer.SetActive(true);
    }

    public void BackESCMenu()
    {
        SettingLayer.SetActive(false);
        escMenuLayer.SetActive(true);
        
    }

    public void NextStage()
    {
        SceneManager.LoadScene("Upgrade Station");
        Time.timeScale = 1f;
    }

    public void QuitBtnOnClick()
    {
        gameover.Play();
        StartCoroutine(GAMEOVER());
    }

    public void RetryBtnOnClick()
    {
        Time.timeScale = 1f;
        Respawn();
        hp.Hp = 3;
        deathLayer.SetActive(false);
        gameUILayer.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Retry");
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

    public IEnumerator GAMEOVER()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }

    void Respawn()
    {
        if (Mission.plat1 == true)
        {
            Player.transform.position = respawnPoint[0].transform.position;
        }
        else if (Mission.plat2 == true)
        {
            Player.transform.position = respawnPoint[1].transform.position;
        }
        else if (Mission.plat3 == true)
        {
            Player.transform.position = respawnPoint[2].transform.position;
        }
    }
}
