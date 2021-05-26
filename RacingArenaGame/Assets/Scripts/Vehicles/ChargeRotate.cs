using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRotate : MonoBehaviour
{
    public string side;
    float rotateAmount = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        float charge = transform.root.gameObject.GetComponent<BaseVehicle>().currentCharge;
        if (charge > 0)
        {
            rotateAmount = 100 * charge * (side == "Left" ? 1 : -1);
        }
        else
        {
            rotateAmount *= 0.7f;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotateAmount, transform.localEulerAngles.z);
    }
}
