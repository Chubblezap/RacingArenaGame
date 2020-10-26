using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    public GameObject spawnpointObj;
    public GameObject[] startingVehicles;
    public GameObject playerObj;
    private GameObject infoobj;
    private Transform[] spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        infoobj = GameObject.Find("GameInfo");
        if(infoobj != null)
        {
            spawnpoints = new Transform[spawnpointObj.transform.childCount];
            for (int i = 0; i < spawnpoints.Length; i++)
            {
                spawnpoints[i] = spawnpointObj.transform.GetChild(i);
            }

            SpawnPlayers(infoobj.GetComponent<GameInfo>().players);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayers(int[] players)
    {
        int realplayers = 0;
        for(int i=0; i < players.Length; i++)
        {
            if(players[i] != 0)
            {
                realplayers++;
            }
        }
        GameObject[] playerobjects = new GameObject[realplayers];

        int playernum = 0;
        for(int i=0; i < players.Length; i++)
        {
            if(players[i] != 0)
            {
                GameObject newplayer = Instantiate(playerObj);
                newplayer.GetComponent<Player>().playerNum = i + 1;
                // Check if this player is "player 1"
                if(newplayer.GetComponent<Player>().playerNum == infoobj.GetComponent<GameInfo>().leadingPlayer)
                {
                    GetComponent<GameTimer>().leadingPlayer = newplayer;
                }
                // Pick a random spawn point
                int spawnpointindex = Random.Range(0, spawnpoints.Length);
                while(spawnpoints[spawnpointindex] == null)
                {
                    spawnpointindex = Random.Range(0, spawnpoints.Length);
                }
                // Create starting vehicle
                GameObject newvehicle = Instantiate(startingVehicles[players[i]-1], spawnpoints[spawnpointindex].position, Quaternion.identity);
                newplayer.GetComponent<Player>().currentVehicle = newvehicle;
                newvehicle.GetComponent<BaseVehicle>().myPlayer = newplayer.GetComponent<Player>();
                newvehicle.GetComponent<BaseVehicle>().UI = newplayer.GetComponent<Player>().UI;
                playerobjects[playernum] = newplayer;
                playernum++;

                spawnpoints[spawnpointindex] = null;
            }
        }

        if(playerobjects.Length == 2)
        {
            playerobjects[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 1, 0.5f);
            playerobjects[1].GetComponent<Player>().cam.rect = new Rect(0, 0, 1, 0.5f);
        }
        else if(playerobjects.Length == 3)
        {
            for(int i=0; i< playerobjects.Length; i++)
            {
                if(playerobjects[i].GetComponent<Player>().playerNum == 1)
                {
                    playerobjects[i].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if(playerobjects[i].GetComponent<Player>().playerNum == 2)
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
        else if(playerobjects.Length == 4)
        {
            playerobjects[0].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            playerobjects[1].GetComponent<Player>().cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            playerobjects[2].GetComponent<Player>().cam.rect = new Rect(0, 0, 0.5f, 0.5f);
            playerobjects[3].GetComponent<Player>().cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
        }
    }
}
