using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject target;
    public Transform targetTransform;
    public Vector3 moveTo;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTo = targetTransform.position + (targetTransform.up * 1.5f + targetTransform.forward * -2.5f);
        speed = Vector3.Distance(transform.position, moveTo) * Time.deltaTime * 10f;
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed);
        transform.LookAt(targetTransform.position + new Vector3(0f,0.5f,0f));
    }
}
