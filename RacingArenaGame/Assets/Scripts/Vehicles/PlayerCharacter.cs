using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Stat modifiers
    [HideInInspector]
    public float ModTopSpeed, ModAcceleration, ModTurn, ModBoost, ModArmor, ModOffense, ModDefense, ModAir;

    // UI
    public GameObject UI;

    // Utilities
    public GameObject cam;
    public GameObject groundcollider;
    public string flag;
    private GameObject gameMaster;
    private Collider myCollider;
    private Rigidbody body;
    private float pilotTimer;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxisRaw("p1Horizontal") != 0 || Input.GetAxisRaw("p1Vertical") != 0)
        {
            Move(Input.GetAxisRaw("p1Horizontal"), Input.GetAxisRaw("p1Vertical"));
        }
        else
        {
            body.velocity *= 0.96f;
        }
        if (body.velocity.magnitude > 3)
        {
            body.velocity *= 0.96f;
        }
        DoDrag();
        if(pilotTimer > 0)
        {
            pilotTimer -= Time.deltaTime;
        }
    }
    
    void Init()
    {
        flag = "Ejecting";
        gameMaster = GameObject.Find("GameController");
        myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        pilotTimer = 3f;
    }

    private void Move(float horizontal, float vertical)
    {
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        // project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // the direction in world space to move
        Vector3 desiredMoveDirection = forward * vertical + right * horizontal;

        // apply the movement
        body.rotation = Quaternion.Slerp(body.rotation, Quaternion.LookRotation(desiredMoveDirection, Vector3.up), 10 * Time.deltaTime);
        body.AddForce(Vector3.Normalize(desiredMoveDirection) * 7 * Time.deltaTime, ForceMode.VelocityChange);
    }

    void DoDrag()
    {
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 0.8f; // lower sideways speed
        body.velocity = body.transform.TransformDirection(localVelocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedobject = collision.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedobject = other.gameObject;
        if (flag == "Ejecting" && collidedobject.tag == "Environment")
        {
            flag = "Controlled";
            groundcollider.GetComponent<SphereCollider>().enabled = true;
        }
        if (flag == "Controlled" && collidedobject.tag == "Vehicle" && pilotTimer < 0)
        {
            Pilot(collidedobject);
        }
    }

    void Pilot(GameObject vehicle)
    {
        BaseVehicle v = vehicle.GetComponent<BaseVehicle>();
        v.player = 1;

        v.cam = cam;
        cam.GetComponent<CamFollow>().target = vehicle;
        cam.GetComponent<CamFollow>().targetTransform = vehicle.transform;
        cam.GetComponent<CamFollow>().mode = "Vehicle";
        cam = null;
        v.UIIinit(UI);
        Destroy(this.gameObject);
    }
}
