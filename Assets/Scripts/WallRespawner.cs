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
using System;

public class WallRespawner : MonoBehaviour
{
    //Array for the x ranges when creating a random number
    private float[] coorValues = new float[2];
    //Both xVal and yVal save the corresponding newly generated coordinates, savedMovement saves the direction and speed of Birb
    private float xval, yVal, savedMovement;

    void Start()
    {
        //Array containing the 2 x values randGen will choose from
        coorValues[0] = -9.3f;
        coorValues[1] = 9.3f;
    }

    //Generate new coordinates for spawning Birb at new position
    private void randGen()
    {
        xval = coorValues[UnityEngine.Random.Range(0, 2)];
        yVal = UnityEngine.Random.Range(0.5f, 3.5f);

        BirbMovement.xVal = xval;
        BirbMovement.yVal = yVal;

        BirbMovement.setFlip = true;
    }

    //If Birb collides with wall, create a new random point with randGen, if Birb was hit
    //by player, stop Birb and wait 3 seconds
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Birb")
        {
            randGen();

            if (BirbMovement.wasHit)
            {
                BirbMovement.wasHit = false;
                savedMovement = BirbMovement.movement.x;
                BirbMovement.movement.x = 0;
                StartCoroutine(waitSome());

            } 
        }
    }

    //Wait for three seconds before re-entering the play view
    IEnumerator waitSome()
    {

        yield return new WaitForSecondsRealtime(3f);

        BirbMovement.movement.x = savedMovement;
        BirbMovement.setFlip = true;
        //isHittable(true);
    }
}

