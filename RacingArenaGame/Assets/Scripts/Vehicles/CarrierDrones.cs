using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierDrones : MonoBehaviour
{
    public GameObject droneObj;
    public float searchRadius = 10f;
    private GameObject[] Drones = new GameObject[2];

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<BaseVehicle>().myPlayer != null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
            foreach(var item in hitColliders)
            {
                if(item.GetComponent<StatPickup>() != null) // Found a stat pickup
                {
                    if(Drones[0] == null && (Drones[1] == null || Drones[1].GetComponent<NeuroDrone>().target != item.gameObject))
                    {
                        Drones[0] = Instantiate(droneObj, transform.position, Quaternion.identity);
                        Drones[0].GetComponent<NeuroDrone>().target = item.gameObject;
                        Drones[0].GetComponent<NeuroDrone>().owner = transform;
                    }
                    else if(Drones[1] == null && (Drones[0].GetComponent<NeuroDrone>().target != item.gameObject))
                    {
                        Drones[1] = Instantiate(droneObj, transform.position, Quaternion.identity);
                        Drones[1].GetComponent<NeuroDrone>().target = item.gameObject;
                        Drones[1].GetComponent<NeuroDrone>().owner = transform;
                    }
                }
            }
        }
    }
}
