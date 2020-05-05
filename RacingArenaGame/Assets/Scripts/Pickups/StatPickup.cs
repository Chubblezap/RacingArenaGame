using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : BaseItem
{
    public string statType;
    public Color pickupColor;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Stat Pickup";
        int typeDecider = Random.Range(1, 8);
        switch (typeDecider)
        {
            case 1:
                statType = "Top Speed";
                pickupColor = new Color(0f, 0.9f, 0.9f);
                break;
            case 2:
                statType = "Acceleration";
                pickupColor = new Color(0.2f, 0, 0.7f);
                break;
            case 3:
                statType = "Turn";
                pickupColor = new Color(0.3f, 0.9f, 0f);
                break;
            case 4:
                statType = "Boost";
                pickupColor = new Color(0.7f, 0f, 0.7f);
                break;
            case 5:
                statType = "Armor";
                pickupColor = new Color(0f, 0.2f, 1f);
                break;
            case 6:
                statType = "Offense";
                pickupColor = new Color(1f, 0.1f, 0f);
                break;
            case 7:
                statType = "Air";
                pickupColor = new Color(0.9f, 0.9f, 0f);
                break;
            default:
                Debug.Log("Wacky case");
                break;
        }
    }
}
