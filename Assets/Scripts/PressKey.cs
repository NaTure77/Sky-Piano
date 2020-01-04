using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKey : MonoBehaviour
{
    public AudioClip source;
    public void Awake()
    {
        source = (AudioClip)Resources.Load(gameObject.name);
    }
}
