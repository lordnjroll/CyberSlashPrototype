using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject inGameLayer;
    [SerializeField] private GameObject startGamebtn;
    [SerializeField] private GameObject quitGamebtn;
    [SerializeField] private GameObject chooseModeLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtnOnClick()
    {
        SceneManager.LoadScene("Arena1");
        Time.timeScale = 1f;
    }
}
