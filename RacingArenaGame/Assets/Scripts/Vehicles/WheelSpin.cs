using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    private GameObject trackedObject;
    public float multiplier = 1;
    private Rigidbody vehicleBody;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = transform.root.gameObject;
        vehicleBody = trackedObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trackedObject.GetComponent<BaseVehicle>().flying == false)
        {
            float rotationAmount = vehicleBody.transform.InverseTransformDirection(vehicleBody.velocity).z * multiplier;
            transform.Rotate(rotationAmount, 0, 0);
        }
    }
}
