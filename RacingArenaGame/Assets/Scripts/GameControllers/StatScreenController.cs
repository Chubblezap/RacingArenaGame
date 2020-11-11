using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatScreenController : MonoBehaviour
{
    public GameObject dataobj;
    public GameObject leadingPlayer;
    private GameObject[] playerlist;

    // Start is called before the first frame update
    void Start()
    {
        dataobj = GameObject.Find("DataCarrier(Clone)");
        playerlist = dataobj.GetComponent<DataCarrier>().orderedPlayers;
        leadingPlayer = dataobj.GetComponent<DataCarrier>().leadingPlayer;
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
        if(leadingPlayer != null && Input.GetButtonDown(leadingPlayer.GetComponent<Player>().startInput))
        {
            StartMinigame();
        }
    }

    void StartMinigame()
    {
        for (int i = 0; i < playerlist.Length; i++)
        {
            if (playerlist[i] == null)
            {
                continue;
            }
            EnableExtraUI(playerlist[i].GetComponent<Player>().cam.transform.GetChild(0));
            playerlist[i].GetComponent<Player>().statSheet.GetComponent<StatsDisplay>().Hide();

            // Set up cameras
            playerlist[i].GetComponentInChildren<CamFollow>().mode = "Standard";
        }
        SceneManager.LoadScene("DragRace1");
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

    void EnableExtraUI(Transform UIObj)
    {
        for (int i = 0; i < UIObj.childCount; i++)
        {
            if (UIObj.GetChild(i).name != "WeaponBarL" && UIObj.GetChild(i).name != "WeaponBarR")
            {
                UIObj.GetChild(i).gameObject.SetActive(true);
            }
            else if((UIObj.GetChild(i).name == "WeaponBarL" && UIObj.GetComponentInParent<Player>().currentVehicle.GetComponent<GunHandler>().leftGun != null) 
                || (UIObj.GetChild(i).name != "WeaponBarR" && UIObj.GetComponentInParent<Player>().currentVehicle.GetComponent<GunHandler>().rightGun != null))
            {
                UIObj.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}