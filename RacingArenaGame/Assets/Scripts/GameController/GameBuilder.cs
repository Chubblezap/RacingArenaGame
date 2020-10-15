using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    public GameObject spawnpointObj;
    public GameObject[] startingVehicles;
    public GameObject camobj;
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
        GameObject[] cams = new GameObject[realplayers];
        GameObject[] playerobjects = new GameObject[realplayers];

        int camnum = 0;
        for(int i=0; i < players.Length; i++)
        {
            if(players[i] != 0)
            {
                GameObject newplayer = Instantiate(playerObj);
                newplayer.GetComponent<Player>().playerNum = i + 1;
                int spawnpointindex = Random.Range(0, spawnpoints.Length);
                while(spawnpoints[spawnpointindex] == null)
                {
                    spawnpointindex = Random.Range(0, spawnpoints.Length);
                }
                GameObject newvehicle = Instantiate(startingVehicles[players[i]-1], spawnpoints[spawnpointindex].position, Quaternion.identity);
                cams[camnum] = Instantiate(camobj, newvehicle.transform.position + Vector3.up, Quaternion.identity);
                newplayer.GetComponent<Player>().currentVehicle = newvehicle;
                newplayer.GetComponent<Player>().playerCam = cams[camnum];
                newvehicle.GetComponent<BaseVehicle>().myPlayer = newplayer.GetComponent<Player>();
                newvehicle.GetComponent<BaseVehicle>().cam = cams[camnum];
                newvehicle.GetComponent<BaseVehicle>().UI = cams[camnum].transform.GetChild(0).gameObject;
                cams[camnum].GetComponent<CamFollow>().target = newvehicle;
                cams[camnum].GetComponent<CamFollow>().targetTransform = newvehicle.transform;
                camnum++;

                spawnpoints[spawnpointindex] = null;
            }
        }

        if(cams.Length == 2)
        {
            cams[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
            cams[1].GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
        }
        else if(cams.Length == 3)
        {
            for(int i=0; i<cams.Length; i++)
            {
                if(cams[i].GetComponent<CamFollow>().target.GetComponent<BaseVehicle>().myPlayer.playerNum == 1)
                {
                    cams[i].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if(cams[i].GetComponent<CamFollow>().target.GetComponent<BaseVehicle>().myPlayer.playerNum == 2)
                {
                    cams[i].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (cams[i].GetComponent<CamFollow>().target.GetComponent<BaseVehicle>().myPlayer.playerNum == 3)
                {
                    cams[i].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                }
                else if (cams[i].GetComponent<CamFollow>().target.GetComponent<BaseVehicle>().myPlayer.playerNum == 4)
                {
                    cams[i].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                }
            }
        }
        else if(cams.Length == 4)
        {
            cams[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            cams[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            cams[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
            cams[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
        }
    }
}
