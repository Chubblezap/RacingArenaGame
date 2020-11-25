using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private float myTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doFadeIn(float timer)
    {
        myTimer = timer;
        StartCoroutine("FadeIn");
    }

    public void doFadeOut(float timer)
    {
        myTimer = timer;
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        float curtime = 0;
        while (curtime < myTimer)
        {
            GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), curtime / myTimer);
            curtime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
    }

    IEnumerator FadeOut()
    {
        float curtime = 0;
        while (curtime < myTimer)
        {
            GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), curtime / myTimer);
            curtime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
    }
}
