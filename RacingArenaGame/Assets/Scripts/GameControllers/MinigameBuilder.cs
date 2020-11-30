using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameBuilder : MonoBehaviour // Builds and manages minigame elements
{
    public GameObject spawnpointObj;
    private GameObject[] orderedPlayers; // length-4 array. may have 0's. Passed from DataCarrier
    private Transform[] spawnpoints;
    private GameObject dataobj;

    // Timer utility
    private bool timerActive;
    private GameObject timerText;
    private float curTime = 0;
    public string niceTime; // needs to be a global variable to pass to scoreboard

    // Scoreboard organizing
    public GameObject scoreboardDataObj;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = new Transform[spawnpointObj.transform.childCount];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = spawnpointObj.transform.GetChild(i);
        }

        dataobj = GameObject.Find("DataCarrier(Clone)");
        orderedPlayers = dataobj.GetComponent<DataCarrier>().orderedPlayers;
        MovePlayers(orderedPlayers);

        timerText = GameObject.Find("GameTimer");

        GetComponent<PauseMenuHandler>().leadingPlayer = dataobj.GetComponent<DataCarrier>().leadingPlayer;
        GameObject.Find("OverviewCamera").GetComponent<OverviewCamera>().leadPlayer = dataobj.GetComponent<DataCarrier>().leadingPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            curTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(curTime / 60F);
            int seconds = Mathf.FloorToInt(curTime - minutes * 60);
            int centiseconds = Mathf.FloorToInt((currentTime - (minutes * 60) - seconds) * 100);
            string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);

            timerText.GetComponent<Text>().text = niceTime;
        }
    }

    void MovePlayers(GameObject[] playerslots) // Edit of SpawnPlayers() in GameBuilder, players should be attached to DataCarrier and don't need instantiation
    {
        int realplayers = 0;

        // Move player vehicles to their repsective spawn points
        for (int i = 0; i < playerslots.Length; i++)
        {
            if (playerslots[i] != null)
            {
                playerslots[i].GetComponent<Player>().currentVehicle.transform.position = spawnpoints[i].transform.position;
                playerslots[i].GetComponent<Player>().currentVehicle.transform.rotation = spawnpoints[i].transform.rotation;
                realplayers++;
            }
        }

        // Adjust all the player cameras; need a second pass through the player list to ignore null entries
        GameObject[] playerobjects = new GameObject[realplayers];
        int pnum = 0;
        for (int i = 0; i < playerslots.Length; i++)
        {
            if (playerslots[i] != null)
            {
                playerobjects[pnum] = playerslots[i];
                pnum++;
            }
        }
        
        if (playerobjects.Length == 1)
        {
            playerobjects[0].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 1);
        }
        if (playerobjects.Length == 2)
        {
            playerobjects[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 0.5f);
            playerobjects[1].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 0.5f);
        }
        else if (playerobjects.Length == 3)
        {
            for (int i = 0; i < playerobjects.Length; i++)
            {
                if (playerobjects[i].GetComponent<Player>().playerNum == 1)
                {
                    playerobjects[i].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (playerobjects[i].GetComponent<Player>().playerNum == 2)
                {
                    playerobjects[i].GetComponent<Player>().cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (playerobjects[i].GetComponent<Player>().playerNum == 3)
                {
                    playerobjects[i].GetComponent<Player>().cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                }
                else if (playerobjects[i].GetComponent<Player>().playerNum == 4)
                {
                    playerobjects[i].GetComponent<Player>().cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                }
            }
        }
        else if (playerobjects.Length == 4)
        {
            playerobjects[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            playerobjects[1].GetComponent<Player>().cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            playerobjects[2].GetComponent<Player>().cam.rect = new Rect(0, 0, 0.5f, 0.5f);
            playerobjects[3].GetComponent<Player>().cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
        }
    }

    public void StartGame() // Runs as the player cameras start to fade in
    {
        StartCoroutine("TimedStart");
    }

    IEnumerator TimedStart()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        timerActive = true;
        GetComponent<PauseMenuHandler>().enabled = true;
        EnableControls(orderedPlayers);
    }

    void EnableControls(GameObject[] playerlist)
    {
        for (int i = 0; i < playerlist.Length; i++)
        {
            if (playerlist[i] == null)
            {
                continue;
            }
            // Re-enable vehicle controls
            playerlist[i].GetComponent<Player>().currentVehicle.GetComponent<BaseVehicle>().disarmed = false;
            playerlist[i].GetComponent<Player>().currentVehicle.GetComponent<BaseVehicle>().hasControl = true;
            playerlist[i].GetComponent<Player>().currentVehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void EndGame() // Runs as the player cameras start to fade in
    {
        StartCoroutine("TimedEnd");
    }

    IEnumerator TimedEnd()
    {
        GetComponent<PauseMenuHandler>().enabled = false;
        GameObject.Find("Fader").GetComponent<Fader>().doFadeIn(2f);
        yield return new WaitForSecondsRealtime(2f);
        dataobj.GetComponent<DataCarrier>().DeleteVehicles();
        SceneManager.LoadScene("Scoreboard");
    }

    public void CreateScoreboard(Player[] orderedPlayers, string[] playerScores) 
    {
        GameObject scores = Instantiate(scoreboardDataObj);
        scores.GetComponent<ScoreboardData>().leadingPlayer = dataobj.GetComponent<DataCarrier>().leadingPlayer;
        scores.GetComponent<ScoreboardData>().playerOrder = orderedPlayers;
        scores.GetComponent<ScoreboardData>().playerScoreString = playerScores;
    }
}
