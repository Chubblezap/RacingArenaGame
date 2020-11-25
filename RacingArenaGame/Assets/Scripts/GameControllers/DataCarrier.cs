using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public GameObject[] orderedPlayers = new GameObject[4];
    public GameObject leadingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteVehicles() // Mainly used at the end of minigames because player controls are still needed for menus
    {
        for(int i=0; i<orderedPlayers.Length; i++)
        {
            if(orderedPlayers[i] != null)
            {
                orderedPlayers[i].GetComponent<Player>().cam.GetComponent<CamFollow>().enabled = false;
                Destroy(orderedPlayers[i].GetComponent<Player>().currentVehicle);
            }
        }
    }
}
