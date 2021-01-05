using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipCellGlows : MonoBehaviour
{
    public GameObject parentVehicle;
    public PartRotate[] glows;
    private float speed;


    // Update is called once per frame
    void FixedUpdate()
    {
        speed = parentVehicle.GetComponent<Rigidbody>().velocity.magnitude;
        for(int i=0; i < glows.Length; i++)
        {
            glows[i].speed = speed/4;
        }
    }
}
