using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float gameMinutes;
    public int numEvents;
    public GameObject textObject;
    private float gameSeconds;
    private float[] eventTimes;
    private float currentTime;
    private int currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameSeconds = gameMinutes * 60;
        currentTime = gameSeconds;
        eventTimes = new float[numEvents];
        for(int i=0; i<numEvents; i++)
        {
            eventTimes[i] = (gameSeconds / (numEvents + 1)) * (i + 1) + Random.Range(-10, 10);
        }
        currentEvent = numEvents - 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        textObject.GetComponent<Text>().text = niceTime;
        if(currentTime <= eventTimes[currentEvent])
        {
            currentEvent -= 1;
        }
        if(currentTime <= 0)
        {
            Debug.Log("End Game");
        }
    }
}
