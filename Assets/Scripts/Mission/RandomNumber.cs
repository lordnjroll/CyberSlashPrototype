using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text targetNumberText;
    [SerializeField] private TMP_Text questionNumberText;
    [SerializeField] private GameObject GameUILayer;
    [SerializeField] private GameObject miniLayer;

    public int targetNumber;
    private Calculator calculator;
    private Mission mission;
    public bool corrent;
    private int questionnum = -1;
    public int maxquestionnum = 3;

    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
        corrent = false;
    }

    void Update()
    {
        if(calculator.Total == targetNumber)
        {
            ChooseNewNumber();
        }

        if (corrent)
        {
            questionnum++;
            corrent = false;
        }

        if (questionnum == maxquestionnum)
        {
            Mission.m_minicount++;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameUILayer.SetActive(true);
            miniLayer.SetActive(false);
            Time.timeScale = 1f;
        }

        questionNumberText.text = "Question " + questionnum + " / " + maxquestionnum;
    }

    void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.Range(0, 255);
        targetNumberText.text = targetNumber.ToString();
        
        corrent = true;
        Debug.Log(corrent);
    }
}
