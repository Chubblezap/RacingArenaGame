using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float MaxHP;
    public float PercentTopSpeedPerMod;
    public float PercentAccelerationPerMod;
    public float PercentTurnPerMod;
    public float PercentBoostPerMod; 
    public float PercentArmorPerMod;
    public float PercentOffensePerMod;
    public float PercentAirPerMod;

    // UI Elements
    public GameObject ChargeBar;
    public GameObject Speedometer;

    // Stat modifiers
    [HideInInspector]
    public float ModTopSpeed, ModAcceleration, ModTurn, ModBoost, ModArmor, ModOffense, ModDefense, ModAir, curHP;
    private float TopSpeedMultiplier, AccelerationMultiplier, TurnMultiplier, BoostMultiplier, ArmorMultiplier, OffenseMultiplier, AirMultiplier;

    // Utilities
    private GameObject gameMaster;
    private float isHolding;
    private float currentCharge;
    private Collider myCollider;
    Rigidbody body;
    private GunHandler gunScript;
    public bool flying = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        isHolding = Input.GetAxis("p1Charge");
        ChargeBar.GetComponent<Image>().fillAmount = currentCharge;
    }

    void FixedUpdate() //put movement/physics stuff here
    {
        Turn(Input.GetAxis("p1Horizontal"), BaseTurn, ModTurn);
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
        if(!flying)
        {
            CheckFlight();
        }
        else
        {
            Aim(Input.GetAxis("p1Vertical"));
        }
    }

    void Init()
    {
        gameMaster = GameObject.Find("GameController");
        //stats
        ModTopSpeed = 0;
        ModAcceleration = 0;
        ModTurn = 0;
        ModBoost = 0;
        ModArmor = 0;
        ModOffense = 0;
        ModAir = 0;
        // stat modifiers
        TopSpeedMultiplier = BaseTopSpeed * PercentTopSpeedPerMod;
        AccelerationMultiplier = BaseAcceleration * PercentAccelerationPerMod;
        TurnMultiplier = BaseTurn * PercentTurnPerMod;
        BoostMultiplier = BaseBoost * PercentBoostPerMod;
        ArmorMultiplier = BaseArmor * PercentArmorPerMod;
        OffenseMultiplier = BaseOffense * PercentOffensePerMod;
        AirMultiplier = BaseAir * PercentAirPerMod;
        curHP = MaxHP;
        // utility
        isHolding = 0;
        currentCharge = 0;
        myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        gunScript = GetComponent<GunHandler>();
    }

    void Turn(float direction, float bTurn, float mTurn) //direction is a value between -1 and 1
    {
        body.AddTorque(Vector3.up * (bTurn + (TurnMultiplier * mTurn)) * direction);
    }

    void Aim(float direction)
    {
        float rotationmax;
        if(direction > 0)
        {
            rotationmax = 45;
        }
        else if(direction < 0)
        {
            rotationmax = 315;
        }
        else
        {
            rotationmax = 0;
        }
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        Debug.Log(currentRotation);
        //currentRotation.z = Mathf.Clamp(currentRotation.z, -30, 30);
        //transform.localRotation = Quaternion.Euler(currentRotation);
        if(!(Mathf.Abs(currentRotation.x - rotationmax) < 5))
        {
            body.AddRelativeTorque(Vector3.right * direction * 5);
        }
    }

    void Accelerate(float bAcceleration, float mAcceleration, float bTopSpeed, float mTopSpeed)
    {

        body.AddForce(transform.forward * (bAcceleration + (AccelerationMultiplier * mAcceleration)));
        if(body.velocity.magnitude > (bTopSpeed + (TopSpeedMultiplier * mTopSpeed)))
        {
            body.velocity *= .96f;
        }
    }

    void Boost(float bBoost, float mBoost)
    {
        body.AddForce(transform.forward * 50 * (bBoost + (BoostMultiplier * mBoost)));
    }

    void Charge(float bArmor, float mArmor, float bBoost, float mBoost)
    {
        body.velocity *= (1f - (0.003f*(bArmor + (ArmorMultiplier * mArmor))));
        currentCharge += 0.0015f * (bBoost + (BoostMultiplier * mBoost));
    }

    void DoDrag(float bArmor, float mArmor)
    {
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 1f - (0.001f*(bArmor + (ArmorMultiplier * mArmor))); // lower sideways speed
        body.velocity =  body.transform.TransformDirection(localVelocity);
    }

    void CheckFlight()
    {
        if(!Physics.Raycast(transform.position, Vector3.up*-1, 1f, LayerMask.GetMask("Environment"), QueryTriggerInteraction.Ignore))
        {
            flying = true;
        }
    }

    void PickupItem(GameObject item)
    {
        if (item.GetComponent<BaseItem>().itemType == "Stat Pickup")
        {
            switch (item.GetComponent<StatPickup>().statType)
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
                    Debug.Log("Got an invalid stat! Yay!");
                    break;
            }
        }
        else if (item.GetComponent<BaseItem>().itemType == "Power Pickup")
        {
            switch (item.GetComponent<PowerPickup>().powerType)
            {
                default:
                    Debug.Log("Picked up a power");
                    break;
            }
        }
        else if (item.GetComponent<BaseItem>().itemType == "Gun Pickup")
        {
            gunScript.PickupGun(item);
        }
        Destroy(item);
    }

    void takeForceHit(float force, float damage, Vector3 forceposition)
    {
        body.AddForce(Vector3.Normalize(transform.position - forceposition) * force);
        curHP -= damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.GetComponent<BaseItem>() != null) // trigger collision is an item
        {
            PickupItem(collidedObject);
        }
        if (collidedObject.GetComponent<BasicProjectile>() != null && collidedObject.GetComponent<BasicProjectile>().owner != transform) // trigger collision is an enemy projectile
        {
            takeForceHit(collidedObject.GetComponent<BasicProjectile>().force, collidedObject.GetComponent<BasicProjectile>().damage, collidedObject.transform.position);
            collidedObject.GetComponent<BasicProjectile>().Detonate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(flying && collidedObject.tag == "Environment")
        {
            flying = false;
            transform.rotation = Quaternion.Euler(new Vector3 (0, transform.rotation.eulerAngles.y, 0));
        }
    }
}
