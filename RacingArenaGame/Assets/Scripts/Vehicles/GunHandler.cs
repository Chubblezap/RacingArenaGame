using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private GameObject gameMaster;
    private BaseVehicle vehicleScript;
    // Guns
    public GameObject carriedGun;
    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject leftGunPosition;
    public GameObject rightGunPosition;

    // the object that is spawned when a gun is picked up
    public GameObject carriedGunObject;

    // the object that is spawned when a gun is dropped
    public GameObject droppedGunObject;

    // Start is called before the first frame update
    void Start()
    {
        carriedGun = null;
        gameMaster = GameObject.Find("GameController");
        vehicleScript = GetComponent<BaseVehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vehicleScript.myPlayer != null && !vehicleScript.disarmed)
        {
            DoGuns();
        }
    }

    void DoGuns()
    {
        if (carriedGun != null)
        {
            if(carriedGun.GetComponent<HeldGun>().flag == "Hovering") // player has an overhead gun
            {
                if (Input.GetButtonDown(vehicleScript.myPlayer.fireRightInput) || Input.GetAxisRaw(vehicleScript.myPlayer.fireRightInput) >= .3)
                {
                    carriedGun.transform.rotation = transform.rotation;
                    carriedGun.GetComponent<HeldGun>().moveTarget = rightGunPosition;
                    carriedGun.GetComponent<HeldGun>().flag = "Slotted";
                    carriedGun.GetComponent<HeldGun>().side = "Right";
                }
                else if (Input.GetButtonDown(vehicleScript.myPlayer.fireLeftInput) || Input.GetAxisRaw(vehicleScript.myPlayer.fireLeftInput) >= .3)
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
                if (Input.GetButton(vehicleScript.myPlayer.fireRightInput) || Input.GetAxisRaw(vehicleScript.myPlayer.fireRightInput) >= .3)
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
                if (Input.GetButton(vehicleScript.myPlayer.fireLeftInput) || Input.GetAxisRaw(vehicleScript.myPlayer.fireLeftInput) >= .3)
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
        GameObject newgun = Instantiate(carriedGunObject, item.transform.position, Quaternion.identity);
        newgun.GetComponent<HeldGun>().gunType = item.GetComponent<GunPickup>().gunType;
        newgun.GetComponent<HeldGun>().owner = transform;
        newgun.transform.SetParent(transform);
    }

    public void DropGun(GameObject gun)
    {
        GameObject drop = Instantiate(droppedGunObject, gun.transform.position, Quaternion.identity);
        drop.GetComponent<GunPickup>().gunType = gun.GetComponent<HeldGun>().gunType;
        drop.GetComponent<Rigidbody>().AddForce(transform.forward * -0.9f + transform.up * 0.9f, ForceMode.Impulse);
        Destroy(gun);
    }
}
