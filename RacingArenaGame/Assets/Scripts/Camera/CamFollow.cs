using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Vector3 moveTo;
    [HideInInspector]
    public string mode = "Standard";
    private GameObject curObject;
    private Transform curTransform;
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
        if(mode == "Standard")
        {
            curObject = myPlayer.currentVehicle;
            float height = 0.75f;
            if (curObject.tag == "Vehicle")
            {
                curTransform = curObject.GetComponent<BaseVehicle>().rotationModel.transform;
                float camdist = -2f - (1f * curObject.GetComponent<BaseVehicle>().currentSpeed / curObject.GetComponent<BaseVehicle>().GetMaxSpeed());
                moveTo = curTransform.position + (Vector3.up * 1.5f + Vector3.Normalize(new Vector3(curTransform.forward.x, 0, curTransform.forward.z )) * camdist) * curObject.GetComponent<BaseVehicle>().camsize;
                height = curObject.GetComponent<BaseVehicle>().camheight;
            }
            else if (curObject.tag == "Player")
            {
                curTransform = curObject.transform;
                moveTo = curTransform.position + (curTransform.up * 1.5f) + (new Vector3(Vector3.Normalize(transform.position - curTransform.position).x * 3, 0, Vector3.Normalize(transform.position - curTransform.position).z * 3));
            }
            else
            {
                Debug.Log("Unknown camtarget tag");
            }
            speed = Vector3.Distance(transform.position, moveTo) * Time.deltaTime * 30f;
            transform.position = Vector3.MoveTowards(transform.position, moveTo, speed);
            transform.LookAt(curTransform.position + new Vector3(0f, height, 0f));
        }
        else if(mode == "StatScreen")
        {
            curObject = myPlayer.currentVehicle;
            curTransform = myPlayer.currentVehicle.transform;
            transform.position = curTransform.position + (new Vector3(0f, 0.5f, 2.5f));
            transform.LookAt(curTransform.position);
        }
    }

    /*public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }*/
}
