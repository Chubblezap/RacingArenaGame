using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewCamController : MonoBehaviour // Contains overview camera movements for DragRace1
{
    private OverviewCamera camComponent;
    private int counter = 1;
    public string minigame;

    // Start is called before the first frame update
    void Start()
    {
        camComponent = GetComponent<OverviewCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(camComponent.ready)
        {
            camComponent.ready = false;
            switch(minigame)
            {
                case "DragRace1":
                    switch (counter)
                    {
                        case 1:
                            camComponent.doSweep(new Vector3(6, 40, 285), new Vector3(6, 40, -130), Quaternion.Euler(45, 180, 0), 7);
                            counter++;
                            break;
                        case 2:
                            camComponent.doPushPull(new Vector3(-36, 47, 60), -150, Quaternion.Euler(35, 110, 0), 5);
                            counter++;
                            break;
                        case 3:
                            camComponent.doMoveAlongCurve(new Vector3(70, 60, 180), new Vector3(150, 60, 45), new Vector3(70, 60, -90), new Vector3(0, 0, 25), 8);
                            counter = 1;
                            break;
                    }
                    break;
                default:
                    Debug.Log("Invalid Minigame ID");
                    break;
            }
        }
    }
}
