using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject MainLayer;
    [SerializeField] private GameObject chooseModeLayer;
    [SerializeField] private GameObject settingLayer;
    [SerializeField] private GameObject CreditLayer;
    [SerializeField] private GameObject DisplayLayer;
    [SerializeField] private GameObject VolumeLayer;
    [SerializeField] private GameObject HTPLayer;

    public int CurrentStageNum = 1;
    public int maxStageNum = 2;
    public TMP_Text Stagetxt;
    public TMP_Text StageInfotxt;

    // Start is called before the first frame update
    void Start()
    {
        DisplayStage();
    }

    public void DisplayStage()
    {
        Stagetxt.text = "Case 1";
        StageInfotxt.text = "Go to work for wage la";
    }

    public void NextStage()
    {
        if (CurrentStageNum < maxStageNum)
        {
            CurrentStageNum++;
            StageSwtich();
        }else if (CurrentStageNum == 2)
        {
            CurrentStageNum = 1;
            StageSwtich();
        }

    }

    public void BeforeStage()
    {
        if (CurrentStageNum > 1)
        {
            CurrentStageNum--;
            StageSwtich();
        }else if (CurrentStageNum == 1)
        {
            CurrentStageNum = 2;
            StageSwtich();
        }
    }

    public void StageSwtich()
    {
        switch (CurrentStageNum)
        {
            case 1:
                Stagetxt.text = "Case 1";
                StageInfotxt.text = "Go to work for wage la";
                break;
            case 2:
                Stagetxt.text = "Case 2";
                StageInfotxt.text = "Dear My Friend, do you have any money?";
                break;
        }
    }

    public void StartBtnOnClick()
    {
        MainLayer.SetActive(false);
        chooseModeLayer.SetActive(true);
        settingLayer.SetActive(false);
    }

    public void GoBtnOnClick()
    {
        switch (CurrentStageNum)
        {
            case 1:
                SceneManager.LoadScene("stage 1");
                break;
            case 2:
                SceneManager.LoadScene("stage 2");
                break;
        }
    }
        


    public void SettingBtnOnClick()
    {
        settingLayer.SetActive(true);
        MainLayer.SetActive(false);
        chooseModeLayer.SetActive(false);
    }

    public void CreditBtnOnClick()
    {
        MainLayer.SetActive(false);
        CreditLayer.SetActive(true);
    }

    public void ExitBtnOnClick()
    {
        CreditLayer.SetActive(false);
        settingLayer.SetActive(false);
        chooseModeLayer.SetActive(false);
        MainLayer.SetActive(true);
    }

    public void QuitBtnOnClick()
    {
        Application.Quit();
    }

    public void DisplayBtnOnClick()
    {
        HTPLayer.SetActive(false);
        VolumeLayer.SetActive(false);
        DisplayLayer.SetActive(true);
    }

    public void VolumeBtnOnClick()
    {
        HTPLayer.SetActive(false);
        DisplayLayer.SetActive(false);
        VolumeLayer.SetActive(true);
    }

    public void HTPBtnOnClick()
    {
        HTPLayer.SetActive(true);
        VolumeLayer.SetActive(false);
        DisplayLayer.SetActive(false);
    }
}
