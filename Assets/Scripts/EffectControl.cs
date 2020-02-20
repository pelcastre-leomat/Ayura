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

public class EffectControl : MonoBehaviour
{
    //Array for holding all audio sources
    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        //Iterate through array to configure all audio sources
        for (int i = 0; i < audioSources.Length; i++)
        {
            float volume = PlayerPrefs.GetFloat("storedFxVolume");
            if (Options.toBool(PlayerPrefs.GetInt("withFx")))
            {
                audioSources[i].volume = volume;
            }
            else
            {
                audioSources[i].volume = 0;
            }
        }
    }
}
