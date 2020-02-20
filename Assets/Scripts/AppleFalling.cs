//#########################################
//#   Game: Ayura                         #
//#   Author: Leonardo Matias Pelcastre   #
//#   Email: lp222nf@student.lnu.se       #
//#   ID: lp222nf                         #
//#   Year: 2019                          #
//#########################################


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleFalling : MonoBehaviour
{
    //Apple Rigidbidy for controlling body type
    public Rigidbody2D apple;

    //Play sound if ground or Cutter are hit
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            apple.tag = "Pickup";
        }else if(col.gameObject.tag == "Cutter")
        {
            fall();
        }
    }

    //Change tag and body type when hit
    private void fall()
    {
        apple.bodyType = RigidbodyType2D.Dynamic;
    }
}
