using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToneChanger : MonoBehaviour
{
    Button button;
    public Color color;
    //public string type;
    public float pitch;
    public void Init(int ID)
    {
        button = GetComponent<Button>();
        GetComponent<Image>().color = color;
        button.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeKeyTone(ID);
            SoundManager.instance.PlaySoundTone();
        });
    }
}
