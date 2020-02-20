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

public class GroundControl : MonoBehaviour
{
    //Sounds effects for different objects
    public AudioSource crashFx,fallenAppleFx;

    //Play the corresponding sound effect according to the object colliding with the ground
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Drop")
        {
            crashFx.Play();
            Destroy(col.gameObject);
        }else if(col.gameObject.tag == "Pickup")
        {
            fallenAppleFx.Play();
        }
    }

    //If a Drop object stays on collision destroy it
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Drop")
        {
            Destroy(col.gameObject);
        }
    }
}
