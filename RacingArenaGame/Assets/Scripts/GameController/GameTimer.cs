using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float gameMinutes;
    public int numEvents;
    private GameObject textObject;
    private GameObject endSlideObject;
    public bool active = false;
    private float gameSeconds;
    private float[] eventTimes;
    private float currentTime;
    private int currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GameObject.Find("GameTimer");
        endSlideObject = GameObject.Find("TimerEndSlider");
        GameObject infoobj = GameObject.Find("GameInfo");
        if(infoobj != null)
        {
            gameMinutes = infoobj.GetComponent<GameInfo>().timeMinutes;
            gameSeconds = gameMinutes*60 + infoobj.GetComponent<GameInfo>().timeSeconds;
        }
        else
        {
            gameSeconds = gameMinutes * 60;
        }
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
        if(active)
        {
            currentTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            textObject.GetComponent<Text>().text = niceTime;
            if (eventTimes.Length > 0 && currentTime <= eventTimes[currentEvent])
            {
                currentEvent -= 1;
            }
            if (currentTime <= 0)
            {
                Debug.Log("End Game");
                EndCityGame();
                active = false;
            }
        }
    }

    void EndCityGame()
    {
        Time.timeScale = 0;
        StartCoroutine("TimerSlide");
        StartCoroutine("EndOfGameBuffer");
    }

    private IEnumerator TimerSlide()
    {
        while (endSlideObject.GetComponent<RectTransform>().position.x < Screen.width/2)
        {
            Vector3 newpos = endSlideObject.GetComponent<RectTransform>().position;
            newpos.x += 25;
            endSlideObject.GetComponent<RectTransform>().position = newpos;
            yield return null;
        }
    }

    IEnumerator EndOfGameBuffer()
    {
        yield return new WaitForSecondsRealtime(5);
        //SceneManager.LoadScene("StatScreen");
        yield return null;
    }
}
