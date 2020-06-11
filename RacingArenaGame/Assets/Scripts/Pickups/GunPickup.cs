using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : BaseItem
{
    public string gunType;
    private int renderedGunIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Gun Pickup";
        int typeDecider = Random.Range(1, 8);
        switch (typeDecider)
        {
            case 1:
                gunType = "Allgear Basic";
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
                gunType = "error";
                break;
        }
        RenderGun(gunType);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 1);
    }

    void RenderGun(string GT)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<GunChildMesh>() != null && transform.GetChild(i).GetComponent<GunChildMesh>().gunName == GT)
            {
                renderedGunIndex = i;
            }
        }
        if (renderedGunIndex != -1)
        {
            transform.GetChild(renderedGunIndex).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
