using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int FerrariChkp { get; set; }
    public int FordChkp { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ferrari")
        {
            FerrariChkp += 1;
        }

        if (collision.tag == "Ford")
        {
            FordChkp += 1;
        }
    }
}
