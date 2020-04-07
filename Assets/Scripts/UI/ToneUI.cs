using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToneUI : MonoBehaviour
{
    public GameObject thisUI;

    private void Start()
    {
        UIManager.menu.toneButton.onClick.AddListener(EnableUI);
    }
    public void EnableUI()
    {
        UIManager.menu.AddCloseAction(DisableUI);
        thisUI.SetActive(true);
    }

    public void DisableUI()
    {
        thisUI.SetActive(false);
    }
}
