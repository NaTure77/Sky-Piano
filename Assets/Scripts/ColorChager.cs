using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChager : MonoBehaviour
{
    Button button;
    public Color color;
    //public string type;
    public float pitch;
    void Start()
    {
        button = GetComponent<Button>();
        GetComponent<Image>().color = color;
        button.onClick.AddListener(() => { StartCoroutine(Functions.instance.StartColorEffect(color)); });
        button.onClick.AddListener(() => { Functions.instance.ChangeKeyTone(pitch); });

    }
}
