using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TMP_Text Totaltxt;
    private BitToggle toggle;

    public int Total { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        var allToggles = FindObjectsOfType<BitToggle>();
        foreach (var toggle in allToggles)
            toggle.OnToggleChanged += Toggle_OnToggleChanged;
    }

    void Toggle_OnToggleChanged(int number, bool enabled)
    {
        if (enabled)
        {
            Total += number;
        }
        else
        {
            Total -= number;
        }
        Totaltxt.text = Total.ToString();
    }
}
