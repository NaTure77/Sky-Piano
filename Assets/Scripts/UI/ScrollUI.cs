using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUI : MonoBehaviour
{
    public Text titleText;
    public GameObject thisUI;
    public GameObject listUI;
    GameObject[] currentList;

    public void EnableUI(string title, GameObject[] lists)
    {
        UIManager.menu.AddCloseAction(DisableUI);
        titleText.text = title;
        for(int i = 0; i < lists.Length; i++)
        {
            lists[i].transform.SetParent(listUI.transform);
            lists[i].transform.localScale = Vector3.one;
        }
        currentList = lists;
        thisUI.SetActive(true);
       
    }

    public void DisableUI()
    {
        thisUI.SetActive(false);
        if (currentList != null)
            for (int i = 0; i < currentList.Length; i++)
            {
                Destroy(currentList[i]);
            }
    }
}
