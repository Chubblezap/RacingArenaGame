using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadSpeed : MonoBehaviour
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
    void FixedUpdate()
    {
        if (trackedObject.GetComponent<BaseVehicle>().flying == false)
        {
            float offset = GetComponent<MeshRenderer>().material.GetTextureOffset("_BaseMap").y;
            float newoffset = offset + vehicleBody.transform.InverseTransformDirection(vehicleBody.velocity).z * multiplier;
            newoffset = newoffset - Mathf.Floor(newoffset);
            GetComponent<MeshRenderer>().material.SetTextureOffset("_BaseMap", new Vector2(0, newoffset));
            //float speed = -vehicleBody.transform.InverseTransformDirection(vehicleBody.velocity).z * multiplier;
            //GetComponent<MeshRenderer>().material.SetVector("_ScrollSpeed", new Vector2(0, speed));
        }
    }
}
