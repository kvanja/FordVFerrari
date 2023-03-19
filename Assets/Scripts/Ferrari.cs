using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ferrari : MonoBehaviour
{
    public float power = 3;
    public float maxspeed = 5;
    public float turnpower = 2;
    public float friction = 3;
    public Vector2 curspeed;
    private Rigidbody2D rigidbody2D;
    private BlindingLights lights;
    private GameObject semafor;
    private AudioSource engineStart;
    private static GameManager gameM;
    public string name;
    public int MapsWon { get; set; }
    public bool GameRunning { get; set; }

    public string Name
    {
        get => name;
        set
        {
            name = value;
        }
    }

    void Start()
    {
        lights = GameObject.FindGameObjectWithTag("semafor").GetComponent<BlindingLights>();
        engineStart = GetComponent<AudioSource>();
        semafor = GameObject.Find("sem_off");
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(CountDown());
        gameM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Name = gameM.FerrariName;
    }

    IEnumerator CountDown()
    {
        int brojac = 5;
        while (brojac > 0)
        {
            lights.ChangeLights(brojac);
            if (brojac == 3)
            {
                engineStart.Play();
            }
            brojac--;
            yield return new WaitForSeconds(1);
            if (brojac == 0)
            {
                GameRunning = true;
            }
        }
        Destroy(semafor);
    }

    void FixedUpdate()
    {
        if (GameRunning)
        {
            curspeed = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);

            if (curspeed.magnitude > maxspeed)
            {
                curspeed = curspeed.normalized;
                curspeed *= maxspeed;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody2D.AddForce(transform.up * power);
                rigidbody2D.drag = friction;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody2D.AddForce(-(transform.up) * (power / 2));
                rigidbody2D.drag = friction;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * turnpower);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.forward * -turnpower);
            }
            noGas();
        }
    }

    void noGas()
    {
        if (!((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))) rigidbody2D.drag = friction * 2;
    }
}
