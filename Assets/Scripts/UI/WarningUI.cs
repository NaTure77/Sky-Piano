using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WarningUI : MonoBehaviour
{
    public GameObject thisUI;
    public Text textUI;
    public Button yesButton;
    public Button noButton;
    public void EnableUI(string text, UnityAction func)
    {
        textUI.text = text;
        thisUI.SetActive(true);
        SetYesButton(func);
        SetNoButton(()=>thisUI.SetActive(false));
    }

    public void SetYesButton(UnityAction func)
    {
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(func);
        yesButton.onClick.AddListener(()=>thisUI.SetActive(false));
    }
    public void SetNoButton(UnityAction func)
    {
        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(func);
    }
}
