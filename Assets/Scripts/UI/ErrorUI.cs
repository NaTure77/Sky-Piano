using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ErrorUI : MonoBehaviour
{
    public GameObject thisUI;
    public Text textUI;
    public Button understandButton;
    public void EnableUI(string text)
    {
        textUI.text = text;
        thisUI.SetActive(true);
        SetUnderstandButton(() =>
        {
            thisUI.SetActive(false);
            //UIManager.back.thisUI.gameObject.SetActive(false);
        });
        //UIManager.back.EnableUI(() => thisUI.SetActive(false));
    }

    public void SetUnderstandButton(UnityAction func)
    {
        understandButton.onClick.RemoveAllListeners();
        understandButton.onClick.AddListener(func);
    }
}
