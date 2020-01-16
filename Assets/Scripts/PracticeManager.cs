using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Functions.instance.MakeKeyboard();
        Functions.instance.SetPlayMode();
    }

}
