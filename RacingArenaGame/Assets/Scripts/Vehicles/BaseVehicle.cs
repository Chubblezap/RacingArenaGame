using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseVehicle : MonoBehaviour
{
    //Base vehicle stats, defined in prefab
    public float BaseTopSpeed;
    public float BaseAcceleration;
    public float BaseTurn;
    public float BaseBoost; //BWAAAAAAAAAAAA
    public float BaseWeight;
    public float BaseOffense;
    public float BaseDefense;

    //Stat modifiers
    [HideInInspector]
    public float ModTopSpeed, ModAcceleration, ModTurn, ModBoost, ModWeight, ModOffense, ModDefense;

    public PlayerInput controls;
    private float isHolding;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() //put movement/physics stuff here
    {
        isHolding = controls.actions["Hold"].ReadValue<float>();
        Turn(controls.actions["Move"].ReadValue<Vector2>().x);
        if (isHolding == 1) // player is holding Space or A
        {
            Charge();
        }
        else
        {
            Accelerate();
        }
    }

    void Init()
    {
        ModTopSpeed = 0;
        ModAcceleration = 0;
        ModTurn = 0;
        ModBoost = 0;
        ModWeight = 0;
        ModOffense = 0;
        ModDefense = 0;
        isHolding = 0;
        controls = GetComponent<PlayerInput>();
    }

    void Turn(float direction) //direction is a value between -1 and 1
    {

    }

    void Accelerate()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * (BaseAcceleration + ModAcceleration));
        if(body.velocity.sqrMagnitude > (BaseTopSpeed + ModTopSpeed))
        {
            body.velocity *= .99f;
        }
    }

    void Charge()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body.velocity.sqrMagnitude < 0.1)
        {
            body.velocity *= .99f;
        }
        else
        {
            body.AddForce(transform.forward * -(BaseWeight + ModWeight));
        }
    }
}
