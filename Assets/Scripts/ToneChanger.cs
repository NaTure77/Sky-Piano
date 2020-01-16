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
    public int ID = 0;
    void Start()
    {
        button = GetComponent<Button>();
        GetComponent<Image>().color = color;
        button.onClick.AddListener(Change);

    }
    public void Change()
    {
        Functions.instance.ColorEffect(color);
        Functions.instance.ChangeKeyTone(pitch);
    }
}
