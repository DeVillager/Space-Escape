using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : Grabbable
{
    public AudioSource audioSource;
    public AudioClip[] crateSounds;
    private AudioClip audioClip;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MuteSourcesForSeconds(2f));
    }

    private IEnumerator MuteSourcesForSeconds(float f)
    {
        audioSource.mute = true;
        yield return new WaitForSeconds(f);
        audioSource.mute = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayRandomSound();
    }
    
    public void PlayRandomSound() {
        if (crateSounds.Length>0) {
            int idx = Random.Range(0,crateSounds.Length-1);
            audioSource.clip = crateSounds[idx];
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }
    }
}