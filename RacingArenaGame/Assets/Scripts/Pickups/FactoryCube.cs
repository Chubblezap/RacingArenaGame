using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCube : BaseItem
{
    public string companyPart = "";
    public string materialPart = "";
    public GameObject[] vehicles;

    private void Start()
    {
        itemType = "Factory Cube";
        pickupTimer = 300f;
    }

    public void TryCraft()
    {
        if(companyPart != "" && materialPart != "")
        {
            CraftVehicle(companyPart, materialPart);
        }
    }

    private void CraftVehicle(string cPart, string mPart)
    {
        if(cPart == "Allgear Material")
        {
            if(mPart == "Mundanium Chunk")
            {
                Instantiate(vehicles[0], transform.position, Quaternion.identity);
            }
            else if(mPart == "Hardite Alloy")
            {
                Instantiate(vehicles[1], transform.position, Quaternion.identity);
            }
            else if(mPart == "Billionvolt Capacitor")
            {
                Instantiate(vehicles[2], transform.position, Quaternion.identity);
            }
            else if(mPart == "Flex Drive")
            {
                Instantiate(vehicles[3], transform.position, Quaternion.identity);
            }
            else if(mPart == "Antimatter Shard")
            {
                Instantiate(vehicles[4], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Invalid material part");
            }
        }
        else if(cPart == "Slicing Edge Material")
        {
            if (mPart == "Mundanium Chunk")
            {
                Instantiate(vehicles[5], transform.position, Quaternion.identity);
            }
            else if (mPart == "Hardite Alloy")
            {
                Instantiate(vehicles[6], transform.position, Quaternion.identity);
            }
            else if (mPart == "Billionvolt Capacitor")
            {
                Instantiate(vehicles[7], transform.position, Quaternion.identity);
            }
            else if (mPart == "Flex Drive")
            {
                Instantiate(vehicles[8], transform.position, Quaternion.identity);
            }
            else if (mPart == "Antimatter Shard")
            {
                Instantiate(vehicles[9], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Invalid material part");
            }
        }
        else if(cPart == "Macrotech Material")
        {
            if (mPart == "Mundanium Chunk")
            {
                Instantiate(vehicles[10], transform.position, Quaternion.identity);
            }
            else if (mPart == "Hardite Alloy")
            {
                Instantiate(vehicles[11], transform.position, Quaternion.identity);
            }
            else if (mPart == "Billionvolt Capacitor")
            {
                Instantiate(vehicles[12], transform.position, Quaternion.identity);
            }
            else if (mPart == "Flex Drive")
            {
                Instantiate(vehicles[13], transform.position, Quaternion.identity);
            }
            else if (mPart == "Antimatter Shard")
            {
                Instantiate(vehicles[14], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Invalid material part");
            }
        }
        else if(cPart == "Neurolink Material")
        {
            if (mPart == "Mundanium Chunk")
            {
                Instantiate(vehicles[15], transform.position, Quaternion.identity);
            }
            else if (mPart == "Hardite Alloy")
            {
                Instantiate(vehicles[16], transform.position, Quaternion.identity);
            }
            else if (mPart == "Billionvolt Capacitor")
            {
                Instantiate(vehicles[17], transform.position, Quaternion.identity);
            }
            else if (mPart == "Flex Drive")
            {
                Instantiate(vehicles[18], transform.position, Quaternion.identity);
            }
            else if (mPart == "Antimatter Shard")
            {
                Instantiate(vehicles[19], transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Invalid material part");
            }
        }
        else
        {
            Debug.Log("Invalid company part");
        }
        Destroy(this.gameObject);
    }

    private void SendParts(GameObject cPart, GameObject mPart)
    {
        cPart.transform.SetParent(transform);
        cPart.GetComponent<HeldPart>().owner = null;
        cPart.GetComponent<HeldPart>().moveTarget = this.gameObject;
        cPart.GetComponent<HeldPart>().flag = "Crafting";

        mPart.transform.SetParent(transform);
        mPart.GetComponent<HeldPart>().owner = null;
        mPart.GetComponent<HeldPart>().moveTarget = this.gameObject;
        mPart.GetComponent<HeldPart>().flag = "Crafting";
    }

    private void CheckVehicleParts(GameObject V)
    {
        PartHandler pHandler = V.GetComponent<PartHandler>();
        if(pHandler.companyPart != null && pHandler.materialPart != null)
        {
            SendParts(pHandler.companyPart, pHandler.materialPart);
            pHandler.companyPart = null;
            pHandler.materialPart = null;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Vehicle")
        {
            CheckVehicleParts(collidedObject);
        }
    }
}
