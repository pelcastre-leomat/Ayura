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
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    //Background music audiosource
    public AudioSource audioSrc;
    //Toggle for turning music on/off
    private Toggle musicToggle;
    //Flag for signaling change has been
    private bool hasChanged;

    //Do not destroy audiosource on new scene load
    private void Awake()
    {
        DontDestroyOnLoad(audioSrc);
    }
    
    void Start()
    {
        //Reset value so as to signal a change from the start
        hasChanged = true;
    }

    void Update()
    {
        //Update the volume and state of the music
        if (hasChanged)
        {
            audioSrc.volume = PlayerPrefs.GetFloat("storedVolume");
            if (Options.toBool(PlayerPrefs.GetInt("withMusic"))) {
                audioSrc.Play();
            }
            hasChanged = false;
        }
    }
}
