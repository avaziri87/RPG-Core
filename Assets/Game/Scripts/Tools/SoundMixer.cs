using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    [SerializeField] AudioSource original;
    [SerializeField] AudioSource newToPlay;
    [SerializeField] float fadeTime = 1f;

    public void FadeOut(AudioClip toPlay)
    {
        StartCoroutine(FadeOutOriginal(toPlay));
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInOriginal());
    }
    public IEnumerator FadeOutOriginal(AudioClip toPlay)
    {
        newToPlay.clip = toPlay;
        newToPlay.volume = 0;
        newToPlay.Play();
        while(!Mathf.Approximately(original.volume, 0))
        {
            original.volume = Mathf.MoveTowards(original.volume, 0, Time.deltaTime / fadeTime);
            newToPlay.volume = Mathf.MoveTowards(newToPlay.volume, 1, Time.deltaTime / fadeTime);
        }
        original.Stop();
        yield return null;
    }
    public IEnumerator FadeInOriginal()
    {
        original.volume = 0;
        original.Play();
        while (!Mathf.Approximately(original.volume, 1))
        {
            original.volume = Mathf.MoveTowards(original.volume, 1, Time.deltaTime / fadeTime);
            newToPlay.volume = Mathf.MoveTowards(newToPlay.volume, 0, Time.deltaTime / fadeTime);
        }
        newToPlay.Stop();
        yield return null;
    }
}
