using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MenuItem
{
    public Sprite[] vehicleSprites;
    public int player;
    private int vehicleIndex = 0;
    private string horizInput;
    private string vertInput;
    private string fireInputLeft;
    private string fireInputRight;
    private string menuButton;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
        DoPlayerInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (pushCooldown > 0)
        {
            pushCooldown -= Time.deltaTime;
        }

        if (active && pushCooldown <= 0)
        {
            if(Input.GetButtonDown(fireInputLeft) || Input.GetAxis(horizInput) <= -0.25)
            {
                vehicleIndex -= 1;
                if(vehicleIndex == -1) { vehicleIndex = 3; }
                GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
                pushCooldown = 0.25f;
            }
            else if(Input.GetButtonDown(fireInputRight) || Input.GetAxis(horizInput) >= 0.25)
            {
                vehicleIndex += 1;
                if (vehicleIndex == 4) { vehicleIndex = 0; }
                GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
                pushCooldown = 0.25f;
            }

            if(player == 1) // Only player 1 can start/exit game
            {
                if (Input.GetAxis(menuButton) >= 0.25)
                {
                    // Start Game?
                }
                if (backItem != null)
                {
                    if (Input.GetAxis(menuButton) <= -0.25)
                    {
                        SwitchItems(backItem);
                    }
                }
            }
        }
    }

    void DoPlayerInit()
    {
        switch (player)
        {
            case 1:
                horizInput = "p1menuHorizontal";
                vertInput = "p1menuVertical";
                fireInputLeft = "p1FireLeft";
                fireInputRight = "p1FireRight";
                menuButton = "p1menuButton";
                break;
            case 2:
                horizInput = "p2menuHorizontal";
                vertInput = "p2menuVertical";
                fireInputLeft = "p2FireLeft";
                fireInputRight = "p2FireRight";
                menuButton = "p2menuButton";
                break;
            case 3:
                horizInput = "p3menuHorizontal";
                vertInput = "p3menuVertical";
                fireInputLeft = "p3FireLeft";
                fireInputRight = "p3FireRight";
                menuButton = "p3menuButton";
                break;
            case 4:
                horizInput = "p4menuHorizontal";
                vertInput = "p4menuVertical";
                fireInputLeft = "p4FireLeft";
                fireInputRight = "p4FireRight";
                menuButton = "p4menuButton";
                break;
            default:
                horizInput = "p1menuHorizontal";
                vertInput = "p1menuVertical";
                fireInputLeft = "p1FireLeft";
                fireInputRight = "p1FireRight";
                menuButton = "p1menuButton";
                break;
        }
    }
}
