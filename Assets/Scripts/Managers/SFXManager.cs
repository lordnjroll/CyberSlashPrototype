using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public AudioSource walk, dash, jump;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            walk.Play();

            //Dash sound
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    dash.Play();
            //}

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    jump.Play();
            //}
        }
    }
}
