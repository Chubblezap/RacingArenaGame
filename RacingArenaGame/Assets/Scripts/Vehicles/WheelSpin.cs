using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    private GameObject trackedObject;
    public float multiplier = 1;
    private Rigidbody vehicleBody;
    public string type = "Wheel";

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
            switch (type)
            {
                case "X":
                    transform.Rotate(rotationAmount, 0, 0);
                    break;
                case "Y":
                    transform.Rotate(0, rotationAmount, 0);
                    break;
                case "Z":
                    transform.Rotate(0, 0, rotationAmount);
                    break;
                default:
                    break;
            }
        }
    }
}
