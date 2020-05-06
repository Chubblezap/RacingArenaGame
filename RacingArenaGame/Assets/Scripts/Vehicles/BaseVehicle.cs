using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BaseVehicle : MonoBehaviour
{
    // Base vehicle stats, defined in prefab
    public float BaseTopSpeed;
    public float BaseAcceleration;
    public float BaseTurn;
    public float BaseBoost; //BWAAAAAAAAAAAA
    public float BaseArmor;
    public float BaseOffense;
    public float BaseAir;

    // UI Elements
    public GameObject ChargeBar;
    public GameObject Speedometer;

    // Stat modifiers
    [HideInInspector]
    public float ModTopSpeed, ModAcceleration, ModTurn, ModBoost, ModArmor, ModOffense, ModDefense, ModAir;

    // Utilities
    public PlayerInput controls;
    private float isHolding;
    private float currentCharge;
    private Collider myCollider;
    private GameObject carriedGun;
    public GameObject carriedGunObject;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        isHolding = controls.actions["Hold"].ReadValue<float>();
        ChargeBar.GetComponent<Image>().fillAmount = currentCharge;
    }

    void FixedUpdate() //put movement/physics stuff here
    {
        Turn(controls.actions["Move"].ReadValue<Vector2>().x, BaseTurn, ModTurn);
        if (isHolding == 1) // player is holding Space or A
        {
            Charge(BaseArmor, ModArmor, BaseBoost, ModBoost);
        }
        else
        {
            if(currentCharge >= 1)
            {
                Boost(BaseBoost, ModBoost);
            }
            currentCharge = 0;
            Accelerate(BaseAcceleration, ModAcceleration, BaseTopSpeed, ModTopSpeed);
        }
        DoDrag(BaseArmor, ModArmor);
    }

    void Init()
    {
        ModTopSpeed = 0;
        ModAcceleration = 0;
        ModTurn = 0;
        ModBoost = 0;
        ModArmor = 0;
        ModOffense = 0;
        ModAir = 0;
        isHolding = 0;
        currentCharge = 0;
        controls = GetComponent<PlayerInput>();
        myCollider = GetComponent<SphereCollider>();
        carriedGun = null;
    }

    void Turn(float direction, float bTurn, float mTurn) //direction is a value between -1 and 1
    {
        transform.Rotate(0, direction * 0.25f * (bTurn + mTurn), 0, Space.Self);
    }

    void Accelerate(float bAcceleration, float mAcceleration, float bTopSpeed, float mTopSpeed)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * (bAcceleration + mAcceleration));
        if(body.velocity.magnitude > (bTopSpeed + mTopSpeed))
        {
            body.velocity *= .96f;
        }
    }

    void Boost(float bBoost, float mBoost)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.AddForce(transform.forward * 50 * (bBoost + mBoost));
    }

    void Charge(float bArmor, float mArmor, float bBoost, float mBoost)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.velocity *= (1f - (0.003f*(bArmor + mArmor)));
        currentCharge += 0.0015f * (bBoost + mBoost);
    }

    void DoDrag(float bArmor, float mArmor)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 1f - (0.001f*(bArmor + mArmor)); // lower sideways speed
        body.velocity =  body.transform.TransformDirection(localVelocity);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.GetComponent<BaseItem>().itemType == "Stat Pickup")
        {
            switch (collidedObject.GetComponent<StatPickup>().statType)
            {
                case "Top Speed":
                    ModTopSpeed += 1;
                    break;
                case "Acceleration":
                    ModAcceleration += 1;
                    break;
                case "Turn":
                    ModTurn += 1;
                    break;
                case "Boost":
                    ModBoost += 1;
                    break;
                case "Armor":
                    ModArmor += 1;
                    break;
                case "Offense":
                    ModOffense += 1;
                    break;
                case "Air":
                    ModAir += 1;
                    break;
                default:
                    Debug.Log("Got an invalid item! Yay!");
                    break;
            }
            Destroy(collidedObject);
        }
        else if (collidedObject.GetComponent<BaseItem>().itemType == "Power Pickup")
        {
            switch (collidedObject.GetComponent<PowerPickup>().powerType)
            {
                default:
                    Debug.Log("Picked up a power");
                    break;
            }
            Destroy(collidedObject);
        }
        else if (collidedObject.GetComponent<BaseItem>().itemType == "Gun Pickup")
        {
            if(carriedGun != null)
            {
                Destroy(carriedGun);
                carriedGun = null;
            }
            carriedGun = Instantiate(carriedGunObject, collidedObject.transform.position, Quaternion.identity);
            carriedGun.GetComponent<HeldGun>().gunType = collidedObject.GetComponent<GunPickup>().gunType;
            carriedGun.GetComponent<HeldGun>().owner = transform;
            Destroy(collidedObject);
        }
    }
}
