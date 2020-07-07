using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject target;
    public Transform targetTransform;
    public Vector3 moveTo;
    public string mode;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        mode = "Vehicle";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == "Vehicle")
        {
            moveTo = targetTransform.position + (targetTransform.up * 1.5f + targetTransform.forward * -2.5f);
        }
        else if(mode == "Player")
        {
            moveTo = targetTransform.position + (targetTransform.up * 1.5f) + (new Vector3(Vector3.Normalize(transform.position - targetTransform.position).x*3, 0, Vector3.Normalize(transform.position - targetTransform.position).z*3));
        }
        speed = Vector3.Distance(transform.position, moveTo) * Time.deltaTime * 10f;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed);
        transform.LookAt(targetTransform.position + new Vector3(0f, 0.5f, 0f));
    }

    /*public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }*/
}
