using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxleTurn : MonoBehaviour
{
    public string position;
    public bool tread = false;
    private Transform rotationObject;
    private GameObject trackedObject;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = transform.root.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TurnAxle();
    }

    private void TurnAxle()
    {
        float finalY = 0;
        float turnRate = trackedObject.GetComponent<BaseVehicle>().turnAmount; // between -1 and 1
        if (position == "R1" || position == "L1")
        {
            
            finalY = (tread ? 15 : 45) * turnRate;
        }
        else if (position == "R2" || position == "L2")
        {
            finalY = -15 * turnRate;
        }
        
        var ray = Physics.Raycast(transform.position, Vector3.up * -1f, out RaycastHit rayhit, 1.5f, LayerMask.GetMask("Environment"), QueryTriggerInteraction.Ignore);
        if (ray && Mathf.Abs(Vector3.SignedAngle(rayhit.normal, Vector3.up, transform.right)) < 45f)
        {
            transform.up -= (transform.up - rayhit.normal) * 0.1f;
            transform.Rotate(transform.root.rotation.eulerAngles);
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, finalY, transform.localEulerAngles.z);
    }
}
