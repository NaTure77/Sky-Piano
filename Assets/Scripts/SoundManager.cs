using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject origin;
    Queue<Audio> queue = new Queue<Audio>();
    int queueSize = 20;
    public static SoundManager instance;

    public void Awake()
    {
        instance = this;
        for(int i = 0; i < queueSize; i++)
        {
            GameObject t = GameObject.Instantiate(origin);
            queue.Enqueue(t.GetComponent<Audio>());
        }
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
}
