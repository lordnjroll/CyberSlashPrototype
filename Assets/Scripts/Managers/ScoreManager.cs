using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private int DesiredScore;
    public Text scorecount;
    public int score;
    float scoreTime;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        
        if(DesiredScore - score > 10)
        {
            score += 1;
        }else if(DesiredScore != score)
        {
            score = DesiredScore;
        }
        scoreTime += Time.deltaTime;
        
        
        scorecount.text = "Score: " + score.ToString();

        
    }

    public void GetScore()
    {
        DesiredScore += 200;
    }
}
