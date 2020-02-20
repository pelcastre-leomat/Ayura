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

public class SpawnedObject : MonoBehaviour
{
    //Multiplyier set by Difficulty, sets how fast an object falls
    public static float speedMult;
    // Start is called before the first frame update
    void Start()
    {
        //Change the velocity of spawned item to the new velocity
        Rigidbody2D item = gameObject.GetComponent<Rigidbody2D>();
        item.velocity = new Vector2(0, -5f * speedMult);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroy spawned object if it collides with anything other than Birb
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Birb")
        {
            Destroy(gameObject);
        }
    }

    //Destroy spawned object if it stays in collision with anything other than Birb
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag != "Birb")
        {
            Destroy(gameObject);
        }
    }
}
