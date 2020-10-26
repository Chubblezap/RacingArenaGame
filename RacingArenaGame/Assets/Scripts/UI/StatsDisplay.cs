﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    private bool showing = false;
    private float target;

    private void Start()
    {
        
    }

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
            StartCoroutine("SlideOut");
        }
    }

    private IEnumerator SlideIn()
    {
        while (GetComponent<RectTransform>().anchoredPosition.x < 200)
        {
            Vector3 newpos = GetComponent<RectTransform>().anchoredPosition;
            newpos.x += 25;
            GetComponent<RectTransform>().anchoredPosition = newpos;
            yield return null;
        }
        showing = true;
    }

    private IEnumerator SlideOut()
    {
        while (GetComponent<RectTransform>().anchoredPosition.x > -200)
        {
            Vector3 newpos = GetComponent<RectTransform>().anchoredPosition;
            newpos.x -= 25;
            GetComponent<RectTransform>().anchoredPosition = newpos;
            yield return null;
        }
        showing = false;
    }
}
