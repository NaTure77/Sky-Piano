using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressKey : MonoBehaviour
{
    AudioClip clip;
    public Image touchedImage;
    public Image buttonImage;
    IEnumerator touchCoroutine;
    public int ID;
    public System.Action PressFunc;
    public System.Action PointerUpFunc;

    Color defaultColor;
    Color touchedColor;
    public void Init(int id, string type)
    {
        ID = id;
        touchCoroutine = touchEffect();
        SetAudioClip(type);
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) =>
        {
            PressFunc();
        });
        eventTrigger.triggers.Add(entry_PointerDown);
        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerUp.callback.AddListener((data) =>
        {
            PointerUpFunc();
        });
        eventTrigger.triggers.Add(entry_PointerUp);
        defaultColor = touchedImage.color;
        touchedColor = touchedImage.color;
        touchedColor.a = 1;
    }
    public void SetPlayMode()
    {
        //UpdateState();
        PressFunc = PressDefault;
        PointerUpFunc = () => {/* if (!ComposeManager.instance.selectedBit.GetBit(ID))*/ DIsableTouched(); };
        //SetSelectedColor(0);
    }
    public void UpdateState(bool b)
    {
        if (b) EnableTouched();
        else DIsableTouched();
    }
    void SetSelectedColor(float a)
    {
        Color color = touchedImage.color;
        color.a = a;
        touchedImage.color = color;
    }
    void EnableTouched()
    {
        touchedImage.color = touchedColor;
    }
    void DIsableTouched()
    {
        touchedImage.color = defaultColor;
    }
    public void SetAudioClip(string t)
    {
        clip = (AudioClip)Resources.Load(t + "/" + ID);
    }

    public void PressDefault()
    {
        Functions.instance.PlaySound(clip);
        EnableTouched();
        //StopCoroutine(touchCoroutine);
        //touchCoroutine = touchEffect();
        //StartCoroutine(touchCoroutine);
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

    public void StartChangeColor(Color color)
    {
        StartCoroutine(ChangeColor(color));
    }
    IEnumerator ChangeColor(Color color)
    {
        float t = 0;
        float time = 1f;

        Color from = buttonImage.color;
        while (t < time)
        {
            buttonImage.color = Color.Lerp(from, color, t);
            t += Time.deltaTime / time;
            yield return null;
        }
        buttonImage.color = color;
    }
}
