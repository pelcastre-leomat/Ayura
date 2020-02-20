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

public class ScoreScript : MonoBehaviour
{
    //Amount of apples picked
    public static int applesPicked;
    //Text element for displaying the score
    public TMP_Text score;
    //Flag for signaling change has been made
    public static bool hasChanged;

    void Start()
    {
        //Reset all variables
        applesPicked = 0;
        hasChanged = true;

    }

    void Update()
    {
        //Update the text only when the value has actually changed
        if (hasChanged)
        {
            score.text = "Score: " + applesPicked;
            hasChanged = false;
        }
    }

    //Increase the score by the given amount
    public static void increaseScore(int score)
    {
        applesPicked+=score;
        hasChanged = true; ;
    }
}
