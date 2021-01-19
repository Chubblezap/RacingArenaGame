using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineChargeVisual : MonoBehaviour
{
    private GameObject trackedObject;

    void Start()
    {
        trackedObject = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_Arc1", 360 - trackedObject.GetComponent<BaseVehicle>().currentCharge * 360);
    }
}
