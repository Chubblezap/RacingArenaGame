using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : BaseItem
{
    public string powerType;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Power Pickup";
        int typeDecider = Random.Range(1, 8);
        switch (typeDecider)
        {
            case 1:
                powerType = "example 1";
                break;
            case 2:
                powerType = "example 2";
                break;
            case 3:
                powerType = "example 3";
                break;
            case 4:
                powerType = "example 4";
                break;
            case 5:
                powerType = "example 5";
                break;
            case 6:
                powerType = "example 6";
                break;
            case 7:
                powerType = "example 7";
                break;
            default:
                Debug.Log("Wacky case");
                break;
        }
    }
}
