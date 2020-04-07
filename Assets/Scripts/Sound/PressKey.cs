using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressKey : MonoBehaviour
{
    public Image touchedImage;
    public Image buttonImage;
    public Text text;
    //IEnumerator touchCoroutine;
    public int ID;
    public System.Action PressFunc;
    public System.Action PointerUpFunc;

    Color defaultColor;
    Color touchedColor;
    EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
    public void Init(int id)
    {
        ID = id;
        //touchCoroutine = touchEffect();
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

       
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
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        //UpdateState();
        PressFunc = PressDefault;
        PointerUpFunc = () => {/* if (!ComposeManager.instance.selectedBit.GetBit(ID))*/ DIsableTouched(); };
        //SetSelectedColor(0);
    }
    public void UpdateState(bool b, Color color)
    {
        if (b)
        {
            EnableTouched(color);
        }
        else DIsableTouched();
    }
    void EnableTouched(Color color)
    {
        touchedImage.color = color;
    }
    void DIsableTouched()
    {
        touchedImage.color = defaultColor;
    }
    public void PressDefault()
    {
        EnableTouched(touchedColor);
        SoundManager.instance.PlaySound(ID);
    }
    public IEnumerator ChangeColor(Color color)
    {
        float t = 0;
        float time = 1f;
        Color from = buttonImage.color;
        color.a = from.a;
        while (t < time)
        {
            buttonImage.color = Color.Lerp(from, color, t);
            t += Time.deltaTime / time;
            yield return null;
        }
        buttonImage.color = color;
    }
}
