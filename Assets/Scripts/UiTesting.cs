using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTesting : MonoBehaviour
{
    [Header("Level Bar Setting")]
    [SerializeField] private Image LevelBarSprite;
    [Range(0, 6)]
    [SerializeField] private int LevelBarLevel = 0;
    [SerializeField] private float LevelBarCurrentValue;
    [SerializeField] private float LevelBarDevideValue = 20f;
    [SerializeField] private int LevelBarMin = 0;
    [SerializeField] private int LevelBarMax = 120;
    [SerializeField] private Gradient LevelBarGradient;

    [Header("Skill Bar Setting")]
    [SerializeField] private Image SkillBarSprite;
    [Range(0, 6)]
    [SerializeField] private int SkillBarLevel = 0;
    [SerializeField] private float SkillBarCurrentValue;
    [SerializeField] private float SkillBarDevideValue = 20f;
    [SerializeField] private int SkillBarMin = 0;
    [SerializeField] private int SkillBarMax = 120;



    // Update is called once per frame
    void Update()
    {
        LevelBarFill();
        SkillBarFill();
    }

    public void LevelBarFill()
    {
        LevelBarCurrentValue = (float)LevelBarLevel * LevelBarDevideValue;
        LevelBarCurrentValue = Mathf.Clamp(LevelBarCurrentValue, LevelBarMin, LevelBarMax);
        LevelBarSprite.fillAmount = LevelBarCurrentValue / LevelBarMax;
        LevelBarSprite.color = LevelBarGradient.Evaluate(LevelBarSprite.fillAmount);
    }

    public void SkillBarFill()
    {
        SkillBarCurrentValue = (float)SkillBarLevel * SkillBarDevideValue;
        SkillBarCurrentValue = Mathf.Clamp(SkillBarCurrentValue, SkillBarMin, SkillBarMax);
        SkillBarSprite.fillAmount = SkillBarCurrentValue / SkillBarMax;
    }
}

