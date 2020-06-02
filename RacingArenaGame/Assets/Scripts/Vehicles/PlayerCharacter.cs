using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Stat modifiers
    [HideInInspector]
    public float ModTopSpeed, ModAcceleration, ModTurn, ModBoost, ModArmor, ModOffense, ModDefense, ModAir;

    // Utilities
    public GameObject cam;
    private GameObject gameMaster;
    private Collider myCollider;
    private Rigidbody body;

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
    }
    
    void Init()
    {
        gameMaster = GameObject.Find("GameController");
        myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
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
}
