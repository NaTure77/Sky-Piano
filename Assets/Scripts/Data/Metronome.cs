using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Metronome : MonoBehaviour
{
    public Button enableButton;
    [System.NonSerialized]
    public double nextTick = 0.0f;
    public AudioClip clip;
    [System.NonSerialized]
    public bool isEnabled = false;
    public static Metronome instance;
    IEnumerator coroutine;
    string title = "메트로놈 BPM 설정";
    public int bpm = 240;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UIManager.menu.metronomeButton.onClick.AddListener(Enable);
    }
    public void Init(int b)
    {
        bpm = b;
        coroutine = startM();
        enableButton.onClick.AddListener(ToggleMetronome);
    }
    public void Enable()
    {
        UIManager.slider.EnableUI(bpm,720,20, SetValue, title);
        enableButton.gameObject.SetActive(true);
    }
    void SetValue(float data)
    {
        bpm = (int)data;
    }

    public void ToggleMetronome()
    {
        if(isEnabled)
        {
            StopCoroutine(coroutine);
        }
        else
        {
            coroutine = startM();
            StartCoroutine(coroutine);
        }
        isEnabled = !isEnabled;
    }
    private IEnumerator startM()
    {
        double spb = 60.0d / bpm;
        double dspTime = AudioSettings.dspTime;
        nextTick = dspTime + spb;
        while (true)
        {
            spb = 60.0d / bpm;
            dspTime = AudioSettings.dspTime;
            //오디오 시스템의 현재시각이 nextTick보다 크거나 같으면
            if(dspTime > nextTick)
            {
                nextTick += spb;
                //if(nextTick >= AudioSettings.dspTime)
                this.OnTick(nextTick);
            }
            yield return null;
        }
    }

    void OnTick(double time)
    {
        SoundManager.instance.PlayScheduled(clip,time);
    }
}