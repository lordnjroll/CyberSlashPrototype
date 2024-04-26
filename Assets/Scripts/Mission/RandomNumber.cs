using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text targetNumberText;
    [SerializeField] private TMP_Text questionNumberText;
    [SerializeField] private GameObject GameUILayer;
    [SerializeField] private GameObject miniLayer;

    private int targetNumber;
    private Calculator calculator;
    private Mission mission;
    bool corrent = false;
    private int questionnum;
    private int maxquestionnum = 3;

    // Start is called before the first frame update
    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
    }

    // Update is called once per frame
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
            GameUILayer.SetActive(true);
            miniLayer.SetActive(false);
        }

        questionNumberText.text = "Question " + questionnum + " / " + maxquestionnum;
    }

    void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.Range(0, 255);
        targetNumberText.text = targetNumber.ToString();
        corrent = true;
    }
}
