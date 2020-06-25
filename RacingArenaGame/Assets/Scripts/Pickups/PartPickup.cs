using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPickup : BaseItem
{
    public string partName;
    public string partType;
    public bool initial;
    private int renderedPartIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Part Pickup";
        if(initial)
        {
            Init();
        }
    }

    public void Init()
    {
        int typeDecider = Random.Range(1, 10);
        switch (typeDecider)
        {
            case 1:
                partName = "Allgear Material";
                partType = "Company";
                break;
            case 2:
                partName = "Slicing Edge Material";
                partType = "Company";
                break;
            case 3:
                partName = "Macrotech Material";
                partType = "Company";
                break;
            case 4:
                partName = "Neurolink Material";
                partType = "Company";
                break;
            case 5:
                partName = "Mundanium Chunk";
                partType = "Material";
                break;
            case 6:
                partName = "Hardite Alloy";
                partType = "Material";
                break;
            case 7:
                partName = "Billionvolt Capacitor";
                partType = "Material";
                break;
            case 8:
                partName = "Flex Drive";
                partType = "Material";
                break;
            case 9:
                partName = "Antimatter Shard";
                partType = "Material";
                break;
            default:
                Debug.Log("Wacky case");
                break;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 1);
    }

    void RenderPart(string PN)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ItemChildMesh>() != null && transform.GetChild(i).GetComponent<ItemChildMesh>().itemName == PN)
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Environment")
        {
            RenderPart(partName);
        }
    }
}
