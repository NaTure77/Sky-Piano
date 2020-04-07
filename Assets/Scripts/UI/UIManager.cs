using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static SliderUI slider;
    public static ScrollUI scroll;
    public static ErrorUI error;
    public static MenuUI menu;
    public static WarningUI warning;
    public static ToneUI tone;

    public static UIManager instance;
    public Text title;
    public Text indicator;
    private void Awake()
    {
        instance = this;
        slider = GetComponent<SliderUI>();
        scroll = GetComponent<ScrollUI>();
        error = GetComponent<ErrorUI>();
        menu = GetComponent<MenuUI>();
        warning = GetComponent<WarningUI>();
        tone = GetComponent<ToneUI>();
    }

    public void SetTitle(string t)
    {
        title.text = t;
    }
    public void SetIndicator(string t)
    {
        indicator.text = t;
    }
}
