using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBuilder : MonoBehaviour
{
    public GameObject spawnpointObj;
    private GameObject[] orderedPlayers; // length-4 array. may have 0's. Passed from DataCarrier
    private Transform[] spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = new Transform[spawnpointObj.transform.childCount];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            spawnpoints[i] = spawnpointObj.transform.GetChild(i);
        }

        GameObject dataobj = GameObject.Find("DataCarrier(Clone)");
        orderedPlayers = dataobj.GetComponent<DataCarrier>().orderedPlayers;
        MovePlayers(orderedPlayers);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
