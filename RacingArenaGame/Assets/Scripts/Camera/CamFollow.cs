using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private GameObject curObject;
    private Transform curTransform;
    public Vector3 moveTo;
    private float speed;
    private Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curObject = myPlayer.currentVehicle;
        curTransform = myPlayer.currentVehicle.transform;
        if (curObject.tag == "Vehicle")
        {
            float camdist = (-2.5f - (curObject.GetComponent<BaseVehicle>().boostPower / 5));
            moveTo = curTransform.position + (Vector3.up * 1.5f + new Vector3(curTransform.forward.x * camdist, 0, curTransform.forward.z * camdist)) * curObject.GetComponent<BaseVehicle>().camsize;
        }
        else if(curObject.tag == "Player")
        {
            moveTo = curTransform.position + (curTransform.up * 1.5f) + (new Vector3(Vector3.Normalize(transform.position - curTransform.position).x*3, 0, Vector3.Normalize(transform.position - curTransform.position).z*3));
        }
        else
        {
            Debug.Log("Unknown camtarget tag");
        }
        speed = Vector3.Distance(transform.position, moveTo) * Time.deltaTime * 15f;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed);
        transform.LookAt(curTransform.position + new Vector3(0f, 0.5f, 0f));
    }

    /*public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }*/
}
