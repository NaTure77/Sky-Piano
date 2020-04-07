using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeManager : MonoBehaviour
{
    public GridLayoutGroup layoutGroup;
    public GridLayoutGroup layoutGroup_example;
    public RectTransform rectTransform;
    public RectTransform rectTransform_example;
    int pivot;
    public static SizeManager instance;

    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        /*
        slider.onValueChanged.AddListener((data) =>
        {
            //DataManager.SetBpm((int)slider.value);
            Resize(data);
        });
        addButton.onClick.AddListener(() => slider.value++);
        subButton.onClick.AddListener(() => slider.value--);*/
        Resize(DataManager.userData.padSize);
        UIManager.menu.resizeButton.onClick.AddListener(Enable);
    }
    void Resize(float data)
    {
        DataManager.userData.padSize = (int)data;
        layoutGroup.cellSize = Vector2.one * data;
        rectTransform.sizeDelta = new Vector2(data * 5, data * 3);

        layoutGroup_example.cellSize = Vector2.one * data;
        rectTransform_example.sizeDelta = new Vector2(data * 5, data * 3);

       // percentage.text = ((int)(data / 3)).ToString();
    }
    public void Enable()
    {
        UIManager.slider.EnableUI(DataManager.userData.padSize,300,60, Resize, "패드 크기 조절");
        rectTransform_example.gameObject.SetActive(true);
        //slider.value = DataManager.userData.padSize;
    }
}
