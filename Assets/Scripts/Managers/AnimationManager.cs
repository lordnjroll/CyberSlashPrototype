using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator shooterANI;
    [SerializeField] private Animator baseANI;
    [SerializeField] private Animator shieldANI;
    [SerializeField] private Animator muscleANI;


    private void Update()
    {
        
    }

    void shooterSFX()
    {
        shooterANI.SetTrigger("isRunning");
    }
}
