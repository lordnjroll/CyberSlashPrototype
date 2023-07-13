using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scorecount;
    int score;
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
        scoreTime += Time.deltaTime;
        
        
        scorecount.text = "Score: " + score.ToString();

        
    }

    public void GetScore()
    {
        score += 200;
    }
}
