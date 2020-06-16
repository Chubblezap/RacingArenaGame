using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MenuItem
{
    public Sprite[] vehicleSprites;
    private int vehicleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
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
            if(Input.GetButtonDown("p1FireLeft"))
            {
                vehicleIndex -= 1;
                if(vehicleIndex == -1) { vehicleIndex = 3; }
                GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
                pushCooldown = 0.25f;
            }
            else if(Input.GetButtonDown("p1FireRight"))
            {
                vehicleIndex += 1;
                if (vehicleIndex == 4) { vehicleIndex = 0; }
                GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex];
                pushCooldown = 0.25f;
            }

            if (Input.GetAxis("menuButton") >= 0.25)
            {
                // Start Game?
            }
            if (backItem != null)
            {
                if (Input.GetAxis("menuButton") <= -0.25)
                {
                    SwitchItems(backItem);
                }
            }
        }
    }
}
