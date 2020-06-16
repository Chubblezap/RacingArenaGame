using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer : MonoBehaviour
{
    private float target;
    public float slidespeed;

    public void SlideIn()
    {
        Vector3 newpos = GetComponent<RectTransform>().position;
        newpos.x = 50;
        GetComponent<RectTransform>().position = newpos;
        target = 0;
        StartCoroutine("Slide");
    }

    public void SlideOut()
    {
        Vector3 newpos = GetComponent<RectTransform>().position;
        newpos.x = 0;
        GetComponent<RectTransform>().position = newpos;
        target = -50;
        StartCoroutine("Slide");
    }

    private IEnumerator Slide()
    {
        while (GetComponent<RectTransform>().position.x > target)
        {
            Vector3 newpos = GetComponent<RectTransform>().position;
            newpos.x -= slidespeed;
            GetComponent<RectTransform>().position = newpos;
            yield return null;
        }
    }
}
