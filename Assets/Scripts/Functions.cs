using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Functions : MonoBehaviour
{
    public GameObject origin;
    Queue<Audio> queue = new Queue<Audio>();
    int queueSize = 20;
    public static Functions instance;
    public GameObject[] keys;
    public void Awake()
    {
        instance = this;
        for(int i = 0; i < queueSize; i++)
        {
            GameObject t = GameObject.Instantiate(origin);
            queue.Enqueue(t.GetComponent<Audio>());
        }
        keys = GameObject.FindGameObjectsWithTag("Key");
    }
    public void PlaySound(AudioClip source)
    {
        Audio audio = queue.Dequeue();
        audio.Play(source);
        queue.Enqueue(audio);
    }

    public void SwabScene(string name)
    {
        SceneManager.LoadScene(name);
    }



    
    public IEnumerator StartColorEffect(Color color)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            StartCoroutine(ChangeColor(keys[i].GetComponent<Image>(),color));
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator ChangeColor(Image image, Color color)
    {
        float t = 0;
        float time = 1f;

        Color from = image.color;
        while (t < time)
        {
            image.color = Color.Lerp(from, color, t);
            t += Time.deltaTime / time;
            yield return null;
        }
        image.color = color;
    }
    public void ChangeKeyTone(string type)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].GetComponentInParent<PressKey>().SetAudioClip(type);
        }
    }
    public void ChangeKeyTone(float pitch)
    {
        foreach(Audio audio in queue)
        {
            audio.GetComponent<AudioSource>().pitch = pitch;
        }
    }
}
