using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject thisUI;

    public Button thisButton;
    public Button instrumentButton;
    public Button toneButton;
    public Button resizeButton;
    public Button metronomeButton;
    public Button exitButton;

    public Button closeButton;

    Stack<UnityAction> stack = new Stack<UnityAction>();

    public System.Action closeCurrentPanel = null;
    public void DisableUI()
    {
        thisUI.SetActive(false);
        closeCurrentPanel?.Invoke();
    }
    public void EnableUI()
    {
        closeButton.gameObject.SetActive(true);
        thisUI.SetActive(true);
        //AddCloseAction(DisableUI);
        stack.Push(DisableUI);
    }
    private void Start()
    {
        thisButton.onClick.AddListener(EnableUI);
        closeButton.onClick.AddListener(() =>
        {
            stack.Pop()();
            if (stack.Count == 0) closeButton.gameObject.SetActive(false);
        });
    }
    public void AddCloseAction(UnityAction act)
    {
        closeCurrentPanel?.Invoke();
        closeCurrentPanel = ()=> act();
        //stack.Push(act);
        
    }
}
