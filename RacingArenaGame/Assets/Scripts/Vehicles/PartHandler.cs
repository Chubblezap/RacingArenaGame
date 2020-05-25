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
        newPart.GetComponent<HeldPart>().partMesh = gameMaster.GetComponent<InfoDump>().GetPartMesh(item.GetComponent<PartPickup>().partName);
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
}
