using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    public List<PressKey> keys = new List<PressKey>();
    public GameObject keyPrefab;
    public GameObject keysListUI;
    //char[] keyName = { '도', '레', '미', '파', '솔', '라', '시', '도', '레', '미', '파', '솔', '라', '시', '도' };
    string[] keyName2 = { "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab", "A", "Bb", "B"};
    //string[] keyName3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+1", "+2", "+3", "+4", "+5"};
    int[] indexNum = { 0, 2, 4, 5, 7, 9, 11, 0, 2, 4, 5, 7, 9, 11, 0};
    public static KeyboardManager instance;
    public BitArray currentBit = new BitArray(15);
    private void Awake()
    {
        instance = this;
    }
    public void Init()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject newObj = GameObject.Instantiate(keyPrefab);
            PressKey key = newObj.GetComponent<PressKey>();
            keys.Add(key);
            key.Init(i);
            //key.text.text = keyName3[i];
            //key.SetPlayMode();
            //newObj.GetComponentInChildren<Text>().text = keyName2[indexNum[i + SoundManager.instance.toneLevel]];
            newObj.transform.SetParent(keysListUI.transform);
            newObj.transform.localScale = Vector3.one;
            newObj.transform.SetAsLastSibling();
        }
    }
    public void SetPlayMode()
    {
        for (int i = 0; i < 15; i++)
        {
            keys[i].SetPlayMode();
        }
    }
    public void ChangeColor(Color color)
    {
        StartCoroutine(StartColorEffect(color));
    }
    public IEnumerator StartColorEffect(Color color)
    {
        var wait = new WaitForSeconds(0.1f);
        for(int i = 0; i < 15; i++) 
            keys[i].text.text = keyName2[(indexNum[(i) % 15] + SoundManager.instance.toneLevel) % 12];
        for (int i = 0; i < 15; i++)
        {
            StartCoroutine(keys[i].GetComponent<PressKey>().ChangeColor(color));
            
            yield return wait;
        }
        
        //ComposeManager.instance.selectedBoxColor = color;

        //color.a = ComposeManager.instance.mainColor.a;
        //ComposeManager.instance.mainColor = color;
        //InfiniteScroll.instance.UpdateState();
    }
}
