using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject inGameLayer;
    [SerializeField] private GameObject startGamebtn;
    [SerializeField] private GameObject quitGamebtn;
    [SerializeField] private GameObject chooseModeLayer;
    [SerializeField] private GameObject settingLayer;

    public int Stagenum = 1;
    //Stage Text
    public TMP_Text Stagetxt;
    // Start is called before the first frame update
    void Start()
    {
        DisplayStage();
    }

    public void DisplayStage()
    {
        Stagetxt.text = "Stage 1";
    }

    public void NextStage()
    {
        Stagenum++;

        switch (Stagenum)
        {
            case 1:
                Stagetxt.text = "Stage 1";
                break;
            case 2:
                Stagetxt.text = "Stage 2";
                break;
            case 3:
                Stagetxt.text = "Stage 3";
                break;
            case 4:
                Stagetxt.text = "Stage 4";
                break;
            default:
                Stagenum = 0;
                break;
        }

    }

    public void BeforeStage()
    {
        Stagenum--;

        switch (Stagenum)
        {
            case 1:
                Stagetxt.text = "Stage 1";
                break;
            case 2:
                Stagetxt.text = "Stage 2";
                break;
            case 3:
                Stagetxt.text = "Stage 3";
                break;
            case 4:
                Stagetxt.text = "Stage 4";
                break;
            default:
                Stagenum = 5;
                break;
        }
    }

    public void StartBtnOnClick()
    {
        inGameLayer.SetActive(false);
        chooseModeLayer.SetActive(true);
        settingLayer.SetActive(false);
    }

    public void GoBtnOnClick()
    {
        if(Stagenum == 1)
        {
            SceneManager.LoadScene("stage 1");
        }
        
    }

    public void SettingBtnOnClick()
    {
        settingLayer.SetActive(true);
        inGameLayer.SetActive(false);
        chooseModeLayer.SetActive(false);
    }

    public void ExitBtnOnClick()
    {
        settingLayer.SetActive(false);
        chooseModeLayer.SetActive(false);
        inGameLayer.SetActive(true);
    }
}
