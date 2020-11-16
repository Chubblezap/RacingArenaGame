using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    private bool paused = false;
    public GameObject leadingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(leadingPlayer.GetComponent<Player>().startInput))
        {
            Pause();
        }
    }

    void Pause()
    {
        if (!paused)
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

    public void DisplayAllPlayerStats()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerData");
        for (int i = 0; i < players.Length; i++)
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
}
