using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSongEvent : Event 
{
    public AudioClip song;
    public override void Call()
    {
        base.Call();
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = song;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
    }
}
