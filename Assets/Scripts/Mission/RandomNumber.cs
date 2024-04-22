using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text targetNumberText;
    private int targetNumber;
    private Calculator calculator;
    bool corrent = false;

    // Start is called before the first frame update
    void Start()
    {
        calculator = FindObjectOfType<Calculator>();
        corrent = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(calculator.Total == targetNumber)
        {
            Mission.m_minicount++;
            ChooseNewNumber();
        }
    }

    void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.Range(0, 255);
        targetNumberText.text = targetNumber.ToString();
    }
}
