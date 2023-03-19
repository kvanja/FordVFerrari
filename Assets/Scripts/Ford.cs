using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ford : MonoBehaviour
{
    static GameManager gameM;
    public float power = 3;
    public float maxspeed = 5;
    public float turnpower = 2;
    public float friction = 3;
    public Vector2 curspeed;
    Rigidbody2D rigidbody2D;
    public int MapsWon { get; set; }
    public bool gameRunning { get; set; }
    public string name;

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
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(CountDown());
        gameM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Name = gameM.FordName;
    }

    IEnumerator CountDown()
    {
        int brojac = 5;
        while (brojac > 0)
        {
            brojac--;
            yield return new WaitForSeconds(1);
            if (brojac == 0) gameRunning = true;
        }
    }
    void FixedUpdate()
    {
        if (gameRunning)
        {
            curspeed = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);

            if (curspeed.magnitude > maxspeed)
            {
                curspeed = curspeed.normalized;
                curspeed *= maxspeed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rigidbody2D.AddForce(transform.up * power);
                rigidbody2D.drag = friction;
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigidbody2D.AddForce(-(transform.up) * (power / 2));
                rigidbody2D.drag = friction;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * turnpower);
            }
            if (Input.GetKey(KeyCode.D))
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
