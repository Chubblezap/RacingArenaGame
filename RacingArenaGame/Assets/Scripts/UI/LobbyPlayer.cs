using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyPlayer : MenuItem
{
	public LobbyPlayer[] otherPlayers; // List of other players
    public Sprite[] vehicleSprites;
    public Sprite[] openSprites;
    public int player;
    public bool open = false;
    private int vehicleIndex = 1;
    private string horizInput;
    private string vertInput;
    private string fireInputLeft;
    private string fireInputRight;
    private string menuButton;
    private GameObject vehicleSprite;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = openSprites[1];
        vehicleSprite = transform.GetChild(0).gameObject;
        vehicleSprite.SetActive(false);
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
        	if(open)
        	{
        		if(Input.GetButtonDown(fireInputLeft) || Input.GetAxis(horizInput) <= -0.25)
            	{
                	vehicleIndex -= 1;
                	if(vehicleIndex == 0) { vehicleIndex = 4; }
                	vehicleSprite.GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex-1];
                	pushCooldown = 0.25f;
            	}
            	else if(Input.GetButtonDown(fireInputRight) || Input.GetAxis(horizInput) >= 0.25)
            	{
            	    vehicleIndex += 1;
            	    if (vehicleIndex == 5) { vehicleIndex = 1; }
            	    vehicleSprite.GetComponent<SpriteRenderer>().sprite = vehicleSprites[vehicleIndex-1];
            	    pushCooldown = 0.25f;
            	}

            	if (Input.GetAxis(menuButton) <= -0.25)
            	{
            	    Open(false);
            	    pushCooldown = 0.25f;
            	}

            	if(player == 1) // Only player 1 can start/exit game
            	{
            	    if (Input.GetAxis(menuButton) >= 0.25)
            	    {
            	        StartGame();
            	    }
            	}
        	}
        	else
        	{
        		if(Input.GetButtonDown(fireInputLeft) || Input.GetButtonDown(fireInputRight) || Input.GetAxis(menuButton) >= 0.25)
        		{
        			Open(true);
        			pushCooldown = 0.25f;
        		}

        		if(player == 1)
        		{
        			if (backItem != null)
            	    {
            	        if (Input.GetAxis(menuButton) <= -0.25)
            			{
            				pushCooldown = 0.25f;
            	 			SwitchItems(backItem);
            			}
            	    }
        		}
        	}
        }
    }

    void Open(bool x)
    {
    	if(x)
    	{
    		open = true;
    		GetComponent<SpriteRenderer>().sprite = openSprites[0];
    		vehicleSprite.SetActive(true);
    	}
    	else if(!x)
    	{
    		open = false;
    		GetComponent<SpriteRenderer>().sprite = openSprites[1];
    		vehicleSprite.SetActive(false);
    	}
    }

    void StartGame()
    {
    	GameObject infoObject = GameObject.Find("GameInfo");
    	for(int i=0; i<otherPlayers.Length; i++)
    	{
    		if(otherPlayers[i].open)
    		{
                infoObject.GetComponent<GameInfo>().players[otherPlayers[i].player - 1] = otherPlayers[i].vehicleIndex;
    		}
    	}
    	if(open)
    	{
            infoObject.GetComponent<GameInfo>().players[player - 1] = vehicleIndex;
        }
    	infoObject.GetComponent<GameInfo>().timeMinutes = 5;
    	infoObject.GetComponent<GameInfo>().timeSeconds = 0;
    	SceneManager.LoadScene("MainScene");
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
