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
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Main menu gameObject
    public GameObject mainMenu;

    //Load scene 1, i.e. Main game
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    //Quit the game
    public void quitGame()
    {
        Application.Quit();
    }
}
