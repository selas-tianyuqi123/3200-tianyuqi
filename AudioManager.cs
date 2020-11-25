using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //When u get gold, the audio will play
    public AudioClip sound_gold;
    //the plug-in about play music
    public AudioSource playerAudio;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    //play music 
    public void Play(AudioClip a)
    {
        playerAudio.enabled = false;
        playerAudio.clip = a;
        playerAudio.enabled = true;
    }

}
