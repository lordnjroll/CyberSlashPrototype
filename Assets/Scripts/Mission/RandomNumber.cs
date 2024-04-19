using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text targetNumberText;
    private int targetNumber;
    private Calculator calculator;

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
    }

    void ChooseNewNumber()
    {
        targetNumber = UnityEngine.Random.Range(0, 255);
        targetNumberText.text = targetNumber.ToString();
    }
}
