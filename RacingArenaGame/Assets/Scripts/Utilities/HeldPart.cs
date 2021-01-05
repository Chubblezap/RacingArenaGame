using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldPart : MonoBehaviour
{
    public Transform owner;
    public GameObject moveTarget;
    public string partName;
    public string partType;
    private int renderedPartIndex = -1;
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
                        owner.GetComponent<PartHandler>().DropPart(owner.GetComponent<PartHandler>().companyPart);
                    }
                    owner.GetComponent<PartHandler>().companyPart = this.gameObject;
                }
                else if (partType == "Material")
                {
                    if (owner.GetComponent<PartHandler>().materialPart != null)
                    {
                        owner.GetComponent<PartHandler>().DropPart(owner.GetComponent<PartHandler>().materialPart);
                    }
                    owner.GetComponent<PartHandler>().materialPart = this.gameObject;
                }
                GetComponent<TrailRenderer>().enabled = false;
                RenderPart(partName);
                transform.SetParent(moveTarget.transform);
                flag = "Held";
            }
        }
        if (flag == "Held") // part hovering over player
        {
            transform.Rotate(new Vector3(0, 1, 0), 0.75f);
        }
        if (flag == "Crafting") // part moving to factory box
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, curSpeed * 3 * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveTarget.transform.position) < 0.001f)
            {
                if (partType == "Company")
                {
                    moveTarget.GetComponent<FactoryCube>().companyPart = partName;
                }
                if (partType == "Material")
                {
                    moveTarget.GetComponent<FactoryCube>().materialPart = partName;
                }
                moveTarget.GetComponent<FactoryCube>().TryCraft();
                Destroy(this.gameObject);
            }
        }
    }

    void RenderPart(string PN)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ItemChildMesh>().itemName == PN)
            {
                renderedPartIndex = i;
            }
        }
        if (renderedPartIndex != -1)
        {
            transform.GetChild(renderedPartIndex).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
