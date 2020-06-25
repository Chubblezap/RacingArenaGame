using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHandler : MonoBehaviour
{
    private GameObject gameMaster;
    public GameObject companyPart;
    public GameObject materialPart;
    public GameObject companyPartPosition;
    public GameObject materialPartPosition;

    // the object that is spawned when a part is picked up
    public GameObject carriedPartObject;

    // the object that is spawned when a part is dropped
    public GameObject droppedPartObject;

    // Start is called before the first frame update
    void Start()
    {
        companyPart = null;
        materialPart = null;
        gameMaster = GameObject.Find("GameController");
    }

    public void PickupPart(GameObject item)
    {
        GameObject newPart;
        newPart = Instantiate(carriedPartObject, item.transform.position, Quaternion.identity);
        newPart.GetComponent<HeldPart>().partName = item.GetComponent<PartPickup>().partName;
        newPart.GetComponent<HeldPart>().partType = item.GetComponent<PartPickup>().partType;
        newPart.GetComponent<HeldPart>().owner = transform;
        newPart.transform.SetParent(transform);

        if (item.GetComponent<PartPickup>().partType == "Company")
        {
            newPart.GetComponent<HeldPart>().moveTarget = companyPartPosition;
        }
        else if (item.GetComponent<PartPickup>().partType == "Material")
        {
            newPart.GetComponent<HeldPart>().moveTarget = materialPartPosition;
        }
        else
        {
            Debug.Log("Invalid Part!");
        }
    }

    public void DropPart(GameObject part)
    {
        GameObject drop = Instantiate(droppedPartObject, part.transform.position, Quaternion.identity);
        drop.GetComponent<PartPickup>().partName = part.GetComponent<HeldPart>().partName;
        drop.GetComponent<PartPickup>().partType = part.GetComponent<HeldPart>().partType;
        drop.GetComponent<Rigidbody>().AddForce(transform.forward * -0.9f + transform.up * 0.9f, ForceMode.Impulse);
        Destroy(part);
    }
}
