using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private GameObject gameMaster;
    // Guns
    public GameObject carriedGun;
    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject leftGunPosition;
    public GameObject rightGunPosition;

    // the object that is spawned when a gun is picked up
    public GameObject carriedGunObject;

    // Controls
    private string fireLeftInput;
    private string fireRightInput;

    // Start is called before the first frame update
    void Start()
    {
        carriedGun = null;
        gameMaster = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        DoGuns();
    }

    public void LoadControls(int player)
    {
        switch (player)
        {
            case 1:
                fireLeftInput = "p1FireLeft";
                fireRightInput = "p1FireRight";
                break;
            case 2:
                fireLeftInput = "p2FireLeft";
                fireRightInput = "p2FireRight";
                break;
            case 3:
                fireLeftInput = "p3FireLeft";
                fireRightInput = "p3FireRight";
                break;
            case 4:
                fireLeftInput = "p4FireLeft";
                fireRightInput = "p4FireRight";
                break;
            default:
                fireLeftInput = "p1FireLeft";
                fireRightInput = "p1FireRight";
                break;
        }
    }

    void DoGuns()
    {
        if (carriedGun != null)
        {
            if(carriedGun.GetComponent<HeldGun>().flag == "Hovering") // player has an overhead gun
            {
                if (Input.GetButtonDown(fireRightInput))
                {
                    carriedGun.transform.rotation = transform.rotation;
                    carriedGun.GetComponent<HeldGun>().moveTarget = rightGunPosition;
                    Debug.Log(rightGunPosition.transform.localPosition);
                    carriedGun.GetComponent<HeldGun>().flag = "Slotted";
                    carriedGun.GetComponent<HeldGun>().side = "Right";
                }
                else if (Input.GetButtonDown(fireLeftInput))
                {
                    carriedGun.transform.rotation = transform.rotation;
                    carriedGun.GetComponent<HeldGun>().moveTarget = leftGunPosition;
                    carriedGun.GetComponent<HeldGun>().flag = "Slotted";
                    carriedGun.GetComponent<HeldGun>().side = "Left";
                }
            }
            else // carriedgun has some other flag like "Picked Up" or "Slotted"
            {
                return;
            }
        }
        else
        {
            if (rightGun != null)
            {
                if (Input.GetButton(fireRightInput))
                {
                    rightGun.GetComponent<FiringHandler>().active = true;
                }
                else
                {
                    rightGun.GetComponent<FiringHandler>().active = false;
                }
            }
            if (leftGun != null)
            {
                if (Input.GetButton(fireLeftInput))
                {
                    leftGun.GetComponent<FiringHandler>().active = true;
                }
                else
                {
                    leftGun.GetComponent<FiringHandler>().active = false;
                }
            }
        }
    }

    public void PickupGun(GameObject item)
    {
        if (carriedGun != null)
        {
            Destroy(carriedGun);
            carriedGun = null;
        }
        carriedGun = Instantiate(carriedGunObject, item.transform.position, Quaternion.identity);
        carriedGun.GetComponent<HeldGun>().gunType = item.GetComponent<GunPickup>().gunType;
        carriedGun.GetComponent<HeldGun>().owner = transform;
        carriedGun.transform.SetParent(transform);
    }
}
