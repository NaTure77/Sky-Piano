using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class SoundManager : MonoBehaviour
{
    public GameObject toneListUI;
    public GameObject buttonPrefab;
    List<ToneChanger> tones = new List<ToneChanger>();
    AudioClip[] clips = new AudioClip[15];
    public GameObject audioPrefab;
    Queue<AudioSource> queue = new Queue<AudioSource>();
    readonly int queueSize = 20;

    public string instrumentType = "A";
    public int toneLevel = 0;

    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < queueSize; i++)
        {
            queue.Enqueue(Instantiate(audioPrefab).GetComponent<AudioSource>());
        }
    }
    private void Start()
    {
        UIManager.menu.instrumentButton.onClick.AddListener(EnableInstrumentSelect);
    }
    public void InitToneList()
    {
        ToneChanger[] list = toneListUI.GetComponentsInChildren<ToneChanger>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].Init(i);
            tones.Add(list[i]);
        }
    }
    public void SetInstrument(string type)
    {
        instrumentType = type;
        StartCoroutine(LoadInstrumemt(type));
    }
    public void EnableInstrumentSelect()
    {
        Debug.Log("!@");
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Instruments");
        if (di.Exists == false)
        {
            di.Create();
        }
        DirectoryInfo[] searchFolders = di.GetDirectories();
        GameObject[] g = new GameObject[searchFolders.Length];
        for (int i = 0; i < searchFolders.Length; i++)
        {
            //AudioClip clip = searchFolders[i].GetFiles()[i];
            g[i] = Instantiate(buttonPrefab);
            string n = searchFolders[i].Name;
            g[i].GetComponent<Button>().onClick.AddListener(() => 
            {
                SetInstrument(n);
            });
            g[i].GetComponentInChildren<Text>().text = searchFolders[i].Name;
        }
        UIManager.scroll.EnableUI("Select instrument", g);
    }
    IEnumerator LoadInstrumemt(string iName)
    {
        AudioClip[] clips = new AudioClip[15];
        for (int i = 0; i < 15; i++)
        {
            UnityWebRequest w = UnityWebRequestMultimedia.GetAudioClip("file://" + Application.persistentDataPath + "/Instruments/" + iName + "/" + i + ".wav", AudioType.WAV);
            yield return w.SendWebRequest();
            if (w.isNetworkError)
            {
                UIManager.error.EnableUI("Failed to load instrument '" + iName +"'.");
                yield break;
            }
            else
            {
                try
                {
                    clips[i] = DownloadHandlerAudioClip.GetContent(w);
                }
                catch
                {
                    UIManager.error.EnableUI("There's  no Instrument '" + iName +"'.");
                    yield break;
                    //Debug.Log(iName);
                }
            }
        }
        for (int i = 0; i < 15; i++)
            Destroy(this.clips[i]);
        this.clips = clips;
        PlaySoundTone();
    }
    public void PlayBitbox(BitArray data)
    {
        for (int i = 0; i < 15; i++)
        {
            if (data[i])
                PlaySound(i);
        }
    }
    public void PlayBitboxScheduled(BitArray data, double time)
    {
        for (int i = 0; i < 15; i++)
        {
            //keys[i].UpdateState(data[i]);
            if (data[i])
            {
                PlayScheduled(i, time);
            }
        }
    }
    public void PlaySound(int index)
    {
        //Debug.Log("!");
        AudioSource audio = queue.Dequeue();
        audio.Stop();
        audio.clip = clips[index];
        audio.Play();
        queue.Enqueue(audio);
    }
    public void PlayScheduled(int index, double time)
    {
        AudioSource audio = queue.Dequeue();
        audio.Stop();
        audio.clip = clips[index];
        audio.PlayScheduled(time);
        queue.Enqueue(audio);
    }
    public void PlayScheduled(AudioClip clip, double time)
    {
        AudioSource audio = queue.Dequeue();
        audio.Stop();
        audio.clip = clip;
        audio.PlayScheduled(time);
        queue.Enqueue(audio);
    }

    public void PlaySoundTone()
    {
        StartCoroutine(toneSoundCoroutine());
    }
    IEnumerator toneSoundCoroutine()
    {
        var wait = new WaitForSeconds(0.2f);
        for (int i = 0; i < 5; i++)
        {
            if (i < 5) PlaySound(i);
            yield return wait;
        }
    }
    public void StopAllPlay()
    {
        AudioSource audio;
        for (int i = 0; i < queue.Count; i++)
        {
            audio = queue.Dequeue();
            if (audio.isPlaying)
            {
                audio.Stop();
                audio.clip = null;
            }
            queue.Enqueue(audio);
        }
    }

    public void ChangeKeyTone(int level)
    {
        toneLevel = level;
        foreach (AudioSource audio in queue)
        {
            audio.pitch = tones[level].pitch;
        }
        
        KeyboardManager.instance.ChangeColor(tones[level].color);
        
    }
}
