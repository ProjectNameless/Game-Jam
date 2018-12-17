using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnEvent : Event {
    public AudioClip clip;
    public override void Call()
    {
        base.Call();
        AudioSource source = GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        if (next != null)
            StartCoroutine(CallNextWhenFinished(source));
    }
    public IEnumerator CallNextWhenFinished(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        next.Call();
    }
}
