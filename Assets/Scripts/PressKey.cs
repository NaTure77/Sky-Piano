using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressKey : MonoBehaviour
{
    AudioClip clip;
    public Image touchedImage;
    IEnumerator touchCoroutine;
    public string type;
    public static PressKey instance;
    void Awake()
    {
        instance = this;
        touchCoroutine = touchEffect();
        SetAudioClip(type);

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
    }

    public void SetAudioClip(string t)
    {
        clip = (AudioClip)Resources.Load(t + "/" + gameObject.name);
    }


    void OnPointerDown(PointerEventData data)
    {
        Functions.instance.PlaySound(clip);
        StopCoroutine(touchCoroutine);
        touchCoroutine = touchEffect();
        StartCoroutine(touchCoroutine);
    }

    IEnumerator touchEffect()
    {
        Color color = touchedImage.color;
        float fadeTime = 0.4f;
        float temp = 1f;
        while(temp > 0)
        {
            color.a = temp;
            touchedImage.color = color;
            temp -= Time.deltaTime / fadeTime;           
            yield return null;
        }
        color.a = 0;
        touchedImage.color = color;
    }
}
