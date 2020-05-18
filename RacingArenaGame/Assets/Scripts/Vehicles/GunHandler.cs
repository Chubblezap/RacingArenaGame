using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private GameObject gameMaster;
    // Guns
    public GameObject carriedGun;
    public GameObject carriedGunObject;
    public GameObject leftGun;
    public GameObject rightGun;
    public Vector3 leftGunPosition;
    public Vector3 rightGunPosition;

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

    void DoGuns()
    {
        if (carriedGun != null)
        {
            if(carriedGun.GetComponent<HeldGun>().flag == "Hovering") // player has an overhead gun
            {
                if (Input.GetButtonDown("p1FireRight"))
                {
                    carriedGun.transform.rotation = transform.rotation;
                    carriedGun.GetComponent<HeldGun>().moveTarget = rightGunPosition;
                    carriedGun.GetComponent<HeldGun>().flag = "Slotted";
                    carriedGun.GetComponent<HeldGun>().side = "Right";
                }
                else if (Input.GetButtonDown("p1FireLeft"))
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
                if (Input.GetButton("p1FireRight"))
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
                if (Input.GetButton("p1FireLeft"))
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
        carriedGun.GetComponent<HeldGun>().gunMesh = gameMaster.GetComponent<InfoDump>().GetGunMesh(item.GetComponent<GunPickup>().gunType);
        carriedGun.GetComponent<HeldGun>().owner = transform;
        carriedGun.transform.SetParent(transform);
    }
}
