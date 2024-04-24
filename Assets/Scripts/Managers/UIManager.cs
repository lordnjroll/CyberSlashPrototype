using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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
    public AudioSource gameover;
    public AudioMixerSnapshot whenPaused, whenStarted;

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
            whenPaused.TransitionTo(2);
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
            whenStarted.TransitionTo(2);
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
        StartCoroutine("GAMEOVER");
    }

    public void RetryBtnOnClick()
    {
        deathLayer.SetActive(false);
        gameUILayer.SetActive(true);
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

    IEnumerator GAMEOVER()
    {
        yield return new WaitForSeconds(1f);
        gameover.Play();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }
}
