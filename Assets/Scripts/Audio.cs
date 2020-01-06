using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    IEnumerator coroutine;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        coroutine = PlaySound(null);
    }

    public void Play(AudioClip clip)
    {
        StopCoroutine(coroutine);
        coroutine = PlaySound(clip);
        StartCoroutine(coroutine);
    }

    IEnumerator PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
        float playTime = 1;
        while (playTime > 0)
        {
            source.volume = playTime;
            playTime -= Time.deltaTime;
            yield return null;
        }
    }
}
