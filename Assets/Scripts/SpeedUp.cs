using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpeedUp : MonoBehaviour
{
    private Ferrari ferrari;
    private GameObject goFerrari;
    private Ford ford;
    private int minPower;
    private AudioSource speedUpSound;
    void Start()
    {
        goFerrari = GameObject.FindGameObjectWithTag("Ferrari");
        ferrari = goFerrari.GetComponent<Ferrari>();
        minPower = (int)ferrari.power;
        speedUpSound = GameObject.FindGameObjectWithTag("Speed").GetComponent<AudioSource>();
    }

    IEnumerator FerrariSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        ferrari.power = minPower;
        StopCoroutine(FerrariSpeed());
    }

    IEnumerator FordSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        ford.power = minPower;
        StopCoroutine(FordSpeed());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!speedUpSound.isPlaying)
        {
            speedUpSound.Play();
        }
        if (collision.tag == "Ferrari")
        {
            ferrari = collision.gameObject.GetComponent<Ferrari>();
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ferrari.power += (ferrari.power * 20 / 100);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                ferrari.power += (ferrari.power * 20 / 100);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                ferrari.power += (ferrari.power * 20 / 100);
            }
            StartCoroutine(FerrariSpeed());
        }
        if (collision.tag == "Ford")
        {
            ford = collision.gameObject.GetComponent<Ford>();
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ford.power += (ford.power * 20 / 100);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                ford.power += (ford.power * 20 / 100);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                ford.power += (ford.power * 20 / 100);
            }
            StartCoroutine(FordSpeed());
        }
    }
}
