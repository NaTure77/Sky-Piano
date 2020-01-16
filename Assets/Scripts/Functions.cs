using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Functions : MonoBehaviour
{
    public GameObject keysListUI;
    public GameObject toneListUI;

    public GameObject keyPrefab;
    public GameObject audioPrefab;

    public string instrumentType = "C";
    public bool isLandscape = true;
    Queue<Audio> queue = new Queue<Audio>();
    int queueSize = 20;
    public static Functions instance;
    List<PressKey> keys = new List<PressKey>();
    List<ToneChanger> tones = new List<ToneChanger>();
    char[] keyName = { '도', '레', '미', '파', '솔', '라', '시', '도', '레', '미', '파', '솔', '라', '시', '도' };
    public float delay;

    public void Awake()
    {
        instance = this;
        if (isLandscape)
            Screen.orientation = ScreenOrientation.Landscape;
        else Screen.orientation = ScreenOrientation.Portrait;
        //오디오 재생 메모리 큐 만들기.
        for (int i = 0; i < queueSize; i++)
        {
            GameObject t = GameObject.Instantiate(audioPrefab);
            queue.Enqueue(t.GetComponent<Audio>());
        }
        

    }
    public void SelectScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void InitToneList()
    {
        ToneChanger[] list = toneListUI.GetComponentsInChildren<ToneChanger>();
        for(int i = 0; i < list.Length; i++)
        {
            list[i].ID = i;
            tones.Add(list[i]);
        }
    }
    public void SetTone(int level)
    {
        tones[level].Change();
    }
    public void MakeKeyboard()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject newObj = GameObject.Instantiate(keyPrefab);
            PressKey key = newObj.GetComponent<PressKey>();
            keys.Add(key);
            key.Init(i, instrumentType);
            //key.SetPlayMode();
            newObj.GetComponentInChildren<Text>().text += keyName[i];
            newObj.transform.SetParent(keysListUI.transform);
            newObj.transform.localScale = Vector3.one;
            newObj.transform.SetAsLastSibling();
        }
    }
    public void SetInstrument(string type)
    {
        foreach (PressKey key in keys)
            key.SetAudioClip(type);
    }
    public void SetPlayMode()
    {
        for (int i = 0; i < 15; i++)
        {
            keys[i].SetPlayMode();
        }
    }
    public void UpdateKeyboardData(BitArray data)
    {
        for (int i = 0; i < 15; i++)
        {
            keys[i].UpdateState(data[i]);
        }
    }
    public void PlayBitbox(BitArray data)
    {
        for (int i = 0; i < 15; i++)
        {
            //keys[i].UpdateState(data[i]);
            if (data[i])
                keys[i].PressDefault();
        }
    }
    public void PlaySound(AudioClip source)
    {
        Audio audio = queue.Dequeue();
        audio.Play(source);
        queue.Enqueue(audio);
    }
    public void ColorEffect(Color color)
    {
        StartCoroutine(StartColorEffect(color));
    }
    public IEnumerator StartColorEffect(Color color)
    {
        var wait = new WaitForSeconds(0.1f);
        for (int i = 0; i < 15; i++)
        {
            keys[i].GetComponent<PressKey>().StartChangeColor(color);
            yield return wait;
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
