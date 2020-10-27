﻿using System.Collections;
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
    private bool paused = false;
    public GameObject leadingPlayer;
    public GameObject dataCarrierObj;

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

            if(Input.GetButtonDown(leadingPlayer.GetComponent<Player>().startInput))
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        if(!paused)
        {
            Time.timeScale = 0;
            DisplayAllPlayerStats();
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            HideAllPlayerStats();
            paused = false;
        }
    }

    void DisplayAllPlayerStats()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerData");
        for(int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().statSheet.GetComponent<StatsDisplay>().Display();
        }
    }

    void HideAllPlayerStats()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerData");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player>().statSheet.GetComponent<StatsDisplay>().Hide();
        }
    }

    void EndCityGame()
    {
        Time.timeScale = 0;
        PickMinigame();
        StartCoroutine("TimerSlide");
        StartCoroutine("EndOfGameBuffer");
    }

    void PickMinigame()
    {

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
        Time.timeScale = 1;
        GameObject carrier = Instantiate(dataCarrierObj);
        carrier.GetComponent<DataCarrier>().leadingPlayer = leadingPlayer;
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerData");
        for (int i = 0; i < players.Length; i++)
        {
            int thisplayer = players[i].GetComponent<Player>().playerNum;
            carrier.GetComponent<DataCarrier>().orderedPlayers[thisplayer-1] = players[i];
            // Attach player data and player vehicle to data carrier
            players[i].transform.parent = carrier.transform;
            players[i].GetComponent<Player>().currentVehicle.transform.parent = carrier.transform;
            
            // Disable vehicle controls (stat screen)
            players[i].GetComponent<Player>().currentVehicle.GetComponent<BaseVehicle>().disarmed = true;
            players[i].GetComponent<Player>().currentVehicle.GetComponent<BaseVehicle>().hasControl = false;
            players[i].GetComponent<Player>().currentVehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            players[i].GetComponent<Player>().currentVehicle.GetComponent<BaseVehicle>().rotationModel.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Set up cameras
            players[i].GetComponentInChildren<CamFollow>().mode = "StatScreen";
        }
        DisplayAllPlayerStats();
        SceneManager.LoadScene("StatScene");
        yield return null;
    }
}
