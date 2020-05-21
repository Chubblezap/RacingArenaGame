using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPickup : BaseItem
{
    public string partType;
    // Start is called before the first frame update
    void Start()
    {
        itemType = "Part Pickup";
        int typeDecider = Random.Range(1, 10);
        switch (typeDecider)
        {
            case 1:
                partType = "Allgear Material";
                break;
            case 2:
                partType = "Slicing Edge Material";
                break;
            case 3:
                partType = "Macrotech Material";
                break;
            case 4:
                partType = "Nerolink Material";
                break;
            case 5:
                partType = "Mundanium Chunk";
                break;
            case 6:
                partType = "Hardite Alloy";
                break;
            case 7:
                partType = "Billionvolt Capacitor";
                break;
            case 8:
                partType = "Flex Drive";
                break;
            case 9:
                partType = "Antimatter Shard";
                break;
            default:
                Debug.Log("Wacky case");
                break;
        }
    }
}
