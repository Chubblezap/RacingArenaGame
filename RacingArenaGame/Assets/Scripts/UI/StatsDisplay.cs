using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    private bool showing = false;
    private float target;

    public void Display()
    {
        if(!showing)
        {
            StartCoroutine("SlideIn");
        }
    }

    public void Hide()
    {
        if(showing)
        {
            StartCoroutine("Slide");
        }
    }

    private IEnumerator SlideIn()
    {
        while (GetComponent<RectTransform>().position.x < Screen.width / 8)
        {
            Vector3 newpos = GetComponent<RectTransform>().position;
            newpos.x += 25;
            GetComponent<RectTransform>().position = newpos;
            yield return null;
        }
        showing = true;
    }

    private IEnumerator SlideOut()
    {
        while (GetComponent<RectTransform>().position.x < -Screen.width / 4)
        {
            Vector3 newpos = GetComponent<RectTransform>().position;
            newpos.x -= 25;
            GetComponent<RectTransform>().position = newpos;
            yield return null;
        }
        showing = false;
    }
}
