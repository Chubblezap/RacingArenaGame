using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : BaseItem
{
    public string gunType;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Gun Pickup";
        int typeDecider = Random.Range(1, 8);
        switch (typeDecider)
        {
            case 1:
                gunType = "Basic Gun";
                break;
            case 2:
                gunType = "Flamethrower";
                break;
            case 3:
                gunType = "Bomb Cannon";
                break;
            case 4:
                gunType = "Mine Layer";
                break;
            case 5:
                gunType = "Viral Spiral";
                break;
            case 6:
                gunType = "Gearblade Launcher";
                break;
            case 7:
                gunType = "Boost Jumper";
                break;
            default:
                Debug.Log("Wacky case");
                break;
        }
    }
}
