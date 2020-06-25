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
    private GameObject gameMaster;
    private Collider myCollider;
    private Rigidbody body;
    private float pilotTimer;
    private float jumpTimer;
    private bool ejected = true;

    // Controls
    public int player = 0;
    private string horizontal;
    private string vertical;
    private string jump;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != 0 && !ejected)
        {
            if (Input.GetAxisRaw(horizontal) != 0 || Input.GetAxisRaw(vertical) != 0)
            {
                Move(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
            }
            else
            {
                Vector3 tmp = body.velocity;
                tmp.x *= 0.96f;
                tmp.z *= 0.96f;
                body.velocity = tmp;
            }
            if (body.velocity.magnitude > 3)
            {
                Vector3 tmp = body.velocity;
                tmp.x *= 0.96f;
                tmp.z *= 0.96f;
                body.velocity = tmp;
            }
            DoDrag();

            if(Input.GetAxis(jump) >= 0.25 && Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f) && jumpTimer <= 0)
            {
                body.AddForce(Vector3.up * 0.06f, ForceMode.Impulse);
                jumpTimer = 1f;
            }

            DoTimers();
        }
    }
    
    void Init()
    {
        gameMaster = GameObject.Find("GameController");
        myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        pilotTimer = 3f;
    }

    void DoTimers()
    {
        if (pilotTimer > 0)
        {
            pilotTimer -= Time.deltaTime;
        }
        if(jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    public void LoadControls(int newplayernum)
    {
        player = newplayernum;
        switch (player)
        {
            case 1:
                horizontal = "p1Horizontal";
                vertical = "p1Vertical";
                jump = "p1menuButton";
                break;
            case 2:
                horizontal = "p2Horizontal";
                vertical = "p2Vertical";
                jump = "p2menuButton";
                break;
            case 3:
                horizontal = "p3Horizontal";
                vertical = "p3Vertical";
                jump = "p3menuButton";
                break;
            case 4:
                horizontal = "p4Horizontal";
                vertical = "p4Vertical";
                jump = "p4menuButton";
                break;
            default:
                horizontal = "p1Horizontal";
                vertical = "p1Vertical";
                jump = "p1menuButton";
                break;
        }
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
        if(collidedobject.tag == "Environment" || collidedobject.tag == "Destructible")
        {
            ejected = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedobject = other.gameObject;
        if (collidedobject.tag == "Vehicle" && pilotTimer < 0)
        {
            Pilot(collidedobject);
        }
    }

    void Pilot(GameObject vehicle)
    {
        BaseVehicle v = vehicle.GetComponent<BaseVehicle>();
        v.LoadControls(player);

        v.cam = cam;
        cam.GetComponent<CamFollow>().target = vehicle;
        cam.GetComponent<CamFollow>().targetTransform = vehicle.transform;
        cam.GetComponent<CamFollow>().mode = "Vehicle";
        cam = null;
        v.UIIinit(UI);
        Destroy(this.gameObject);
    }
}
