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
using TMPro;

public class HealthScript : MonoBehaviour
{
    //Text element for displaying the health
    public TMP_Text healthIndicator;
    //Health points
    public static float healthPoints;
    //Flag for signaling change has been made
    public static bool hasChanged;

    void Start()
    {
        //Reset all variables
        healthPoints = 100;
        hasChanged = true;
    }

    void Update()
    {
        //If a change to the health points has been made, update the color of the text to inform user of its health 
        //in a more fun way, finally update the text
        if (hasChanged)
        {
            if(healthPoints > 60)
            {
                healthIndicator.color = Color.green;
            }else if(healthPoints > 30)
            {
                healthIndicator.color = Color.yellow;
            }
            else if(healthPoints > -1)
            {
                healthIndicator.color = Color.red;
            }
            healthIndicator.text = "Health: " + healthPoints;
            hasChanged = false;
        }
    }

    //Update health by the given amount, also check if health doesn't go below 0 or above 100
    public static void updateHP(int hp)
    {
        if(hp < 0)
        {
            if((healthPoints + hp) > 0)
            {
                healthPoints += hp;
            }
            else
            {
                healthPoints = 0;
                GameOver.isDead = true;
            }
        }
        else
        {
            if(!((healthPoints + hp) > 100))
            {
                healthPoints += hp;
            }
            else
            {
                healthPoints = 100;
            }
        }
        hasChanged = true;
    }
}
