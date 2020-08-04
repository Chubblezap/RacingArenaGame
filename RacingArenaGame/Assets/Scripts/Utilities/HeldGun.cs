using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldGun : MonoBehaviour
{
    public Transform owner;
    public GameObject moveTarget;
    public string gunType;
    private float curSpeed = 0;
    private bool movingToPlayer = true;
    private int renderedGunIndex = -1;

    //flags
    public string flag = "Picked Up";
    public string side = "None";

    void Update()
    {
        if(curSpeed <= 5)
        {
            curSpeed += Time.deltaTime * 3;
        }
    }

    void FixedUpdate()
    {
        if(owner != null && movingToPlayer == true) // gun picked up, moving to top of player
        {
            float distmod = owner.gameObject.GetComponent<BaseVehicle>().gunhoverdist;
            transform.position = Vector3.MoveTowards(transform.position, owner.position + (new Vector3(0, 0.5f, 0) * distmod), curSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, owner.position + (new Vector3(0f,0.5f,0f) * distmod)) < 0.001f)
            {
                movingToPlayer = false;
                flag = "Hovering";
                if (owner.GetComponent<GunHandler>().carriedGun != null) { owner.GetComponent<GunHandler>().DropGun(owner.GetComponent<GunHandler>().carriedGun); }
                owner.GetComponent<GunHandler>().carriedGun = this.gameObject;

                // rendering hovering gun
                GetComponent<TrailRenderer>().enabled = false;
                RenderGun(gunType);
            }
        }
        if(movingToPlayer == false && flag != "Slotted" && flag != "Equipped") // gun hovering over player
        {
            transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        if(flag == "Slotted") // gun moving to slot
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, curSpeed*3 * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveTarget.transform.position) < 0.001f)
            {
                if (side == "Right")
                {
                    if(owner.GetComponent<GunHandler>().rightGun != null) { owner.GetComponent<GunHandler>().DropGun(owner.GetComponent<GunHandler>().rightGun); }
                    owner.GetComponent<GunHandler>().rightGun = this.gameObject;
                }
                else if(side == "Left")
                {
                    if (owner.GetComponent<GunHandler>().leftGun != null) { owner.GetComponent<GunHandler>().DropGun(owner.GetComponent<GunHandler>().leftGun); }
                    owner.GetComponent<GunHandler>().leftGun = this.gameObject;
                }
                else
                {
                    Debug.Log("Invalid slot side");
                    Destroy(this.gameObject);
                }
                transform.SetParent(moveTarget.transform);
                transform.rotation = moveTarget.transform.parent.rotation;
                GetComponent<FiringHandler>().enabled = true;
                GetComponent<FiringHandler>().GetGunStats();
                flag = "Equipped";
                owner.GetComponent<GunHandler>().carriedGun = null;
                DisableOutlines(transform.GetChild(renderedGunIndex)); //Disable outline
            }
        }
    }

    void RenderGun(string GT)
    {
        for (int i=0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<ItemChildMesh>().itemName == GT)
            {
                renderedGunIndex = i;
            }
        }
        if(renderedGunIndex != -1)
        {
            transform.GetChild(renderedGunIndex).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void DisableOutlines(Transform gun)
    {
        for(int i = 0; i < gun.childCount; i++)
        {
            if(gun.GetChild(i).name != "GunOutline") // The mesh is broken into parts, so the outline is the next child
            {
                gun.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            else // The mesh is solid, so this is the outline
            {
                gun.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
