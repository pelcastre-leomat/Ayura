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

public class AppleSpawner : MonoBehaviour
{
    //Apple Prefab
    public GameObject applePrefab;

    //For identifying the vertices of the triangle
    private Vector2 A, B, C;
    //For checking if a point lies in ABC
    private float w1, w2;
    //Counter for limiting the amount of apples spawned
    public static int applesSpawned;

    void Start()
    {
        //Reset the amount of apples spawned
        applesSpawned = 0;
        //Three vertices of the spawner triangle
        A = new Vector2(-3.549f, -0.43f);
        B = new Vector2(-0.35f, 3.504f);
        C = new Vector2(2.832f, -0.422f);
    }

    // Update is called once per frame 
    void Update()
    {
        //Spawn 10 apples before stopping, respawn an apple immmediately after picked
        if(applesSpawned <= 10)
        {
            generateNewRand();
            applesSpawned++;
        }
    }

    //Generate new coordinates at random for spawning an apple
    private void generateNewRand()
    {
        float rX = Random.Range(-3.549f, 2.832f);
        float rY = Random.Range(-0.43f, 3.504f);
        while(!checkPoint(new Vector2(rX, rY)))
        {
            rX = Random.Range(-3.549f, 2.832f);
            rY = Random.Range(-0.43f, 3.504f);
        }

        Instantiate(applePrefab, new Vector2(rX, rY), Quaternion.identity);
    }

    //Check if the point P lies in the triangle
    private bool checkPoint(Vector2 P)
    {

        float w1 = (A.x * (C.y - A.y) + (P.y - A.y) * (C.x - A.x) - P.x * (C.y - A.y)) / ((B.y - A.y) * (C.x - A.x) - (B.x - A.x) * (C.y - A.y));
        float w2 = (P.y - A.y - (w1 * (B.y - A.y))) / (C.y - A.y);

        return (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1);

    }
}
