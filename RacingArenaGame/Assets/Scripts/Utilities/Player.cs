using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNum = 0;
    public GameObject currentVehicle;
    public Camera cam;
    public GameObject statSheet;
    public GameObject UI;

    // stats
    public int TopSpeed = 0;
    public int Acceleration = 0;
    public int Turn = 0;
    public int Boost = 0;
    public int Armor = 0;
    public int Offense = 0;
    public int Defense = 0;
    public int Air = 0;

    // controls
    [HideInInspector]
    public string horizontalInput, verticalInput, chargeInput, fireLeftInput, fireRightInput, jumpInput, startInput;

    // Start is called before the first frame update
    void Start()
    {
        if(playerNum != 0)
        {
            LoadControls(playerNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadControls(int player)
    {
        switch (player)
        {
            case 1:
                horizontalInput = "p1Horizontal";
                verticalInput = "p1Vertical";
                chargeInput = "p1Charge";
                fireLeftInput = "p1FireLeft";
                fireRightInput = "p1FireRight";
                jumpInput = "p1menuButton";
                startInput = "p1startButton";
                break;
            case 2:
                horizontalInput = "p2Horizontal";
                verticalInput = "p2Vertical";
                chargeInput = "p2Charge";
                fireLeftInput = "p2FireLeft";
                fireRightInput = "p2FireRight";
                jumpInput = "p2menuButton";
                startInput = "p2startButton";
                break;
            case 3:
                horizontalInput = "p3Horizontal";
                verticalInput = "p3Vertical";
                chargeInput = "p3Charge";
                fireLeftInput = "p3FireLeft";
                fireRightInput = "p3FireRight";
                jumpInput = "p3menuButton";
                startInput = "p3startButton";
                break;
            case 4:
                horizontalInput = "p4Horizontal";
                verticalInput = "p4Vertical";
                chargeInput = "p4Charge";
                fireLeftInput = "p4FireLeft";
                fireRightInput = "p4FireRight";
                jumpInput = "p4menuButton";
                startInput = "p4startButton";
                break;
            default:
                Debug.Log("Default player loaded");
                horizontalInput = "p1Horizontal";
                verticalInput = "p1Vertical";
                chargeInput = "p1Charge";
                fireLeftInput = "p1FireLeft";
                fireRightInput = "p1FireRight";
                jumpInput = "p1menuButton";
                startInput = "p1startButton";
                break;
        }
    }
}
