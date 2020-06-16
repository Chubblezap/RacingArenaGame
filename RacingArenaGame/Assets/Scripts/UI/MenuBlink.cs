using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuBlink : MonoBehaviour
{
    public bool active;
    public float fadeSpeed;
    private float timer = 0;
    private Color color;

    private void Start()
    {
        color = GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            timer += Time.deltaTime;
            color.a = (Mathf.Sin(timer / fadeSpeed)) / 2 + 0.5f;
            GetComponent<Text>().color = color;
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
        timer = 0;
        color.a = 1;
        GetComponent<Text>().color = color;
    }
}
