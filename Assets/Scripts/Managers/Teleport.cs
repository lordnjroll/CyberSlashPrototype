using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleport : MonoBehaviour
{
    float Timer = 3;
    [SerializeField] private TMP_Text tipstxt;

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                Timer = 0;
                SceneManager.LoadScene("stage 2");
                Debug.Log("teleport");
            }

            if(Timer <= 3)
            {
                tipstxt.text = "You teleport to next stage in " + (int)Timer;
            }


        }
    }
}
