using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doFadeIn()
    {
        StartCoroutine("FadeIn");
    }

    public void doFadeOut()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        while (GetComponent<Image>().color.a < 1)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, GetComponent<Image>().color.a + 0.0025f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
    }

    IEnumerator FadeOut()
    {
        while (GetComponent<Image>().color.a > 0)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, GetComponent<Image>().color.a - 0.0025f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
    }
}
