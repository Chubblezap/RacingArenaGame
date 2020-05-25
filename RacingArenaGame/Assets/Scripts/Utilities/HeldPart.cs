using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldPart : MonoBehaviour
{
    public Transform owner;
    public GameObject moveTarget;
    public string partName;
    public string partType;
    public Mesh partMesh;
    private float curSpeed = 0;

    //flags
    public string flag = "Picked Up";

    void Update()
    {
        if (curSpeed <= 5)
        {
            curSpeed += Time.deltaTime * 3;
        }
    }

    void FixedUpdate()
    {
        if (owner != null && flag == "Picked Up") // part picked up, moving to position
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, curSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveTarget.transform.position) < 0.001f)
            {
                if ( partType == "Company")
                {
                    if(owner.GetComponent<PartHandler>().companyPart != null)
                    {
                        Destroy(owner.GetComponent<PartHandler>().companyPart);
                    }
                    owner.GetComponent<PartHandler>().companyPart = this.gameObject;
                }
                else if (partType == "Material")
                {
                    if (owner.GetComponent<PartHandler>().materialPart != null)
                    {
                        Destroy(owner.GetComponent<PartHandler>().materialPart);
                    }
                    owner.GetComponent<PartHandler>().materialPart = this.gameObject;
                }
                GetComponent<TrailRenderer>().enabled = false;
                GetComponent<MeshFilter>().mesh = partMesh;
                transform.SetParent(moveTarget.transform);
                flag = "Held";
            }
        }
        if (flag == "Held") // part hovering over player
        {
            transform.Rotate(new Vector3(0, 1, 0), 1);
        }
        if (flag == "Crafting") // part moving to factory box
        {
            /*
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, curSpeed * 3 * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveTarget.transform.position) < 0.001f)
            {
                if (side == "Right")
                {
                    if (owner.GetComponent<GunHandler>().rightGun != null) { Destroy(owner.GetComponent<GunHandler>().rightGun); }
                    owner.GetComponent<GunHandler>().rightGun = this.gameObject;
                }
                else if (side == "Left")
                {
                    if (owner.GetComponent<GunHandler>().leftGun != null) { Destroy(owner.GetComponent<GunHandler>().leftGun); }
                    owner.GetComponent<GunHandler>().leftGun = this.gameObject;
                }
                else
                {
                    Debug.Log("Invalid slot side");
                    Destroy(this.gameObject);
                }
                GetComponent<FiringHandler>().enabled = true;
                GetComponent<FiringHandler>().GetGunStats();
                flag = "Equipped";
                owner.GetComponent<GunHandler>().carriedGun = null;
            }
            */
        }

    }
}
