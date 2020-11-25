using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceGoal : MonoBehaviour
{
    public string type; // "Drag" or "Normal"
    private GameObject[] orderedPlayers;
    private int numplayers = 0;
    private int playercounter = 0;
    private int[] playerLaps;
    private Player[] finishedPlayers; // Players in the order they finished
    private string[] finishTimes; // nicely-formatted times taken from MinigameBuilder

    // Start is called before the first frame update
    void Start()
    {
        GameObject dataobj = GameObject.Find("DataCarrier(Clone)");
        orderedPlayers = dataobj.GetComponent<DataCarrier>().orderedPlayers;
        for(int i=0; i < orderedPlayers.Length; i++)
        {
            if(orderedPlayers[i] != null)
            {
                numplayers++;
            }
        }
        finishedPlayers = new Player[numplayers];
        finishTimes = new string[numplayers];
        if (type == "Drag")
        {
            playerLaps = new int[] { 1, 1, 1, 1 };
        }
        else if (type == "Normal")
        {
            playerLaps = new int[] { 3, 3, 3, 3 };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Player thisplayer = null;
        Debug.Log(collision.gameObject.GetComponent<BaseVehicle>());
        if(collision.gameObject.GetComponent<BaseVehicle>() != null)
        {
            thisplayer = collision.gameObject.GetComponent<BaseVehicle>().myPlayer;
        }
        else if(collision.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            thisplayer = collision.gameObject.GetComponent<PlayerCharacter>().myPlayer;
        }

        if(thisplayer != null && !finishedPlayers.Contains(thisplayer))
        {
            playerLaps[thisplayer.playerNum - 1]--;
            if(playerLaps[thisplayer.playerNum - 1] == 0)
            {
                finishedPlayers[playercounter] = thisplayer;
                finishTimes[playercounter] = GameObject.Find("MiniGameController").GetComponent<MinigameBuilder>().niceTime;
                playercounter++;
            }
        }

        if(playercounter == numplayers)
        {
            GameObject.Find("MiniGameController").GetComponent<MinigameBuilder>().CreateScoreboard(finishedPlayers, finishTimes);
            GameObject.Find("MiniGameController").GetComponent<MinigameBuilder>().EndGame();
        }
    }
}
