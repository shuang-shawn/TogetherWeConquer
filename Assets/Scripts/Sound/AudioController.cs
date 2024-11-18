using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController : MonoBehaviour
{
    public static AudioController aCtrl;
    public AudioSource snipePrep;
    public AudioSource snipeFinish;
    public AudioSource bossSnipeHit;

    public void Awake()
    {
        if (aCtrl == null)
        {
            // levelMusic.loop = true;
            aCtrl = this;
        }
    }
    public void PlaySnipePrep()
    {
        //aCtrl.sfxSrc.Play() //this does the same thing
        snipePrep.Play();
    }
    public void PlaySnipeFinish()
    {
        //aCtrl.sfxSrc.Play() //this does the same thing
        snipeFinish.Play();
    }
    public void PlayBossSnipeHit()
    {
        //aCtrl.sfxSrc.Play() //this does the same thing
        bossSnipeHit.Play();
    }
    // public void StopMusic()
    // {
    //     levelMusic.Stop();
    // }
    // public void PauseMusic()
    // {
    //     levelMusic.Pause();
    // }
    // public void PlayMusic()
    // {
    //     levelMusic.Play();
    // }

    //more functions to dynamically add new sounds
}