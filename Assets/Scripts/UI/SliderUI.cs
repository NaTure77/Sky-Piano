using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public GameObject thisUI;
    public Button add;
    public Button sub;   
    public Text titleText;
    public Text numberText;
    public Slider slider;
    private void Awake()
    {
        add.onClick.AddListener(() => slider.value++);
        sub.onClick.AddListener(() => slider.value--);
    }
    public void EnableUI(int value,int max, int min, UnityAction<float> sliderFunc, string title)
    {
        UIManager.menu.AddCloseAction(DisableUI);
        slider.onValueChanged.RemoveAllListeners();

        slider.maxValue = max;
        slider.minValue = min;
        slider.onValueChanged.AddListener(sliderFunc);
        slider.onValueChanged.AddListener((data)=> { numberText.text = data.ToString(); });
        slider.value = value;
        numberText.text = value.ToString();
        titleText.text = title;
        thisUI.SetActive(true);
        
    }
    public void DisableUI()
    {
        thisUI.SetActive(false);
        Metronome.instance.enableButton.gameObject.SetActive(false);
        SizeManager.instance.rectTransform_example.gameObject.SetActive(false);
    }
}
