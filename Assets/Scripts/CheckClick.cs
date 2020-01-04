using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckClick : MonoBehaviour
{
    public GameObject origin;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase.Equals(TouchPhase.Began))
                {
                    RaycastHit2D hit = Physics2D.Raycast(touch.position, Vector2.zero, 0f);
                    if (hit.collider != null)
                    {
                        StartCoroutine(PlaySound(hit.collider.gameObject.GetComponent<PressKey>().source));
                    }
                }
               
            }
        }
    }
    IEnumerator PlaySound(AudioClip source)
    {
        GameObject a = GameObject.Instantiate(origin);
        
        AudioSource s = a.GetComponent<AudioSource>();
        s.clip = source;
        s.Play();
        float playTime = 1;
        while (playTime > 0)
        {
            s.volume = playTime;
            playTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(a);


    }

    public void SwabScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
