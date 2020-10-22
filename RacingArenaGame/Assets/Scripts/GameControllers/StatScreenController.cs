using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatScreenController : MonoBehaviour
{
    public GameObject dataobj;
    private GameObject[] playerlist;

    // Start is called before the first frame update
    void Start()
    {
        dataobj = GameObject.Find("DataCarrier(Clone)");
        playerlist = dataobj.GetComponent<DataCarrier>().orderedPlayers;
        BuildStatScreen();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<playerlist.Length; i++)
        {
            if(playerlist[i] != null)
            {
                playerlist[i].GetComponent<Player>().currentVehicle.transform.Rotate(0, 0.25f, 0);
            }
        }
    }

    void BuildStatScreen()
    {
        // Arrange player cameras and vehicles
        for (int i=0; i<playerlist.Length; i++)
        {
            if(playerlist[i] == null)
            {
                continue;
            }
            if (i + 1 == 1)
            {
                playerlist[i].GetComponent<Player>().cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                playerlist[i].GetComponent<Player>().currentVehicle.transform.position = new Vector3(5, 5, 2);
            }
            else if (i + 1 == 2)
            {
                playerlist[i].GetComponent<Player>().cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                playerlist[i].GetComponent<Player>().currentVehicle.transform.position = new Vector3(-5, 5, 2);
            }
            else if (i + 1 == 3)
            {
                playerlist[i].GetComponent<Player>().cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                playerlist[i].GetComponent<Player>().currentVehicle.transform.position = new Vector3(5, -5, 2);
            }
            else if (i + 1 == 4)
            {
                playerlist[i].GetComponent<Player>().cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                playerlist[i].GetComponent<Player>().currentVehicle.transform.position = new Vector3(-5, -5, 2);
            }

            DisableExtraUI(playerlist[i].GetComponent<Player>().cam.transform.GetChild(0));
            playerlist[i].GetComponent<Player>().currentVehicle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void DisableExtraUI(Transform UIObj)
    {
        for(int i = 0; i < UIObj.childCount; i++)
        {
            if(UIObj.GetChild(i).name != "Stats")
            {
                UIObj.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}