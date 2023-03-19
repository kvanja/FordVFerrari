using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindingLights : MonoBehaviour
{
    private AudioSource soundOfLights;
    void Start()
    {
        soundOfLights = GameObject.FindGameObjectWithTag("semafor").GetComponent<AudioSource>();
    }

    public void ChangeLights(int number)
    {
        GetComponent<Animator>().SetInteger("Lights", number);
        if (number <= 4 && number > 1)
        {
            soundOfLights.Play();
        }
        else if (number == 1)
        {
            soundOfLights.clip = Resources.Load<AudioClip>("GreenLight");
            soundOfLights.Play();
        }
    }
}