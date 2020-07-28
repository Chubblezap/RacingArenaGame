﻿using System.Collections;
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
    public GameObject UI;
    private GameObject ChargeBar;
    private GameObject ChargeBarFill;
    private GameObject HealthBar;
    private GameObject HealthBarFill;
    private GameObject Speedometer;

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
    private PartHandler partScript;
    private float ejectTimer;
    public int startplayer;
    [HideInInspector]
    public int player;
    public GameObject playerCharacter;
    public GameObject cam;
    public GameObject rotationModel;
    public bool usesDrag = true; // For the Slipcell

    // flight
    private bool flying = false;
    private bool stableFlight = true;
    private float totalFlightTime;
    private float flightTimer;
    private float flightSpeedMultiplier;

    //bouncepad positions
    [HideInInspector]
    public Vector3 bpadstart;
    [HideInInspector]
    public Vector3 bpadend;

    // Controls
    private string horizontalInput;
    private string verticalInput;
    private string chargeInput;
    private string fireLeftInput;
    private string fireRightInput;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != 0)
        {
            isHolding = Input.GetAxis(chargeInput);
            ChargeBarFill.GetComponent<Image>().fillAmount = currentCharge;
            HealthBarFill.GetComponent<Image>().fillAmount = curHP/MaxHP;
        }
    }

    void FixedUpdate() //put movement/physics stuff here
    {
        GroundAlign(BaseAir, ModAir);
        if (player != 0)
        {
            Turn(Input.GetAxis(horizontalInput), BaseTurn, ModTurn);
            if (isHolding == 1) // player is holding Space or A
            {
                Charge(BaseArmor, ModArmor, BaseBoost, ModBoost, BaseTopSpeed, ModTopSpeed);
                if (Input.GetAxis(verticalInput) <= -0.75)
                {
                    ejectTimer += Time.deltaTime;
                    if (ejectTimer >= 1)
                    {
                        currentCharge = 0;
                        Eject();
                    }
                }
                else
                {
                    ejectTimer = 0;
                }
            }
            else
            {
                if (currentCharge > 0)
                {
                    Boost(currentCharge, BaseBoost, ModBoost);
                }
                ejectTimer = 0;
                currentCharge = 0;
                Accelerate(BaseAcceleration, ModAcceleration, BaseTopSpeed, ModTopSpeed);
            }
            if(usesDrag)
            {
                DoDrag(BaseArmor, ModArmor);
            }
            if (flying)
            {
                Aim(Input.GetAxis(verticalInput), BaseAir, ModAir);
                DoFlightGravity();
            }
        }
        else
        {
            body.velocity = new Vector3(body.velocity.x * 0.95f, body.velocity.y, body.velocity.z * 0.95f);
        }
    }

    void Init()
    {
        gameMaster = GameObject.Find("GameController");
        if (UI != null)
        {
            ChargeBar = UI.transform.GetChild(0).gameObject;
            ChargeBarFill = ChargeBar.transform.GetChild(0).gameObject;
            HealthBar = UI.transform.GetChild(1).gameObject;
            HealthBarFill = HealthBar.transform.GetChild(0).gameObject;
            HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHP * 5f, 30f);
            HealthBarFill.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHP * 5f - 10f, 20f);
        }
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
        flightSpeedMultiplier = 1;
        flightTimer = 0;
        totalFlightTime = 0;
        // utility
        isHolding = 0;
        currentCharge = 0;
        ejectTimer = 0;
        myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        gunScript = GetComponent<GunHandler>();
        partScript = GetComponent<PartHandler>();
        LoadControls(startplayer);
    }

    public void LoadControls(int newplayernum)
    {
        player = newplayernum;
        switch (player)
        {
            case 1:
                horizontalInput = "p1Horizontal";
                verticalInput = "p1Vertical";
                chargeInput = "p1Charge";
                break;
            case 2:
                horizontalInput = "p2Horizontal";
                verticalInput = "p2Vertical";
                chargeInput = "p2Charge";
                break;
            case 3:
                horizontalInput = "p3Horizontal";
                verticalInput = "p3Vertical";
                chargeInput = "p3Charge";
                break;
            case 4:
                horizontalInput = "p4Horizontal";
                verticalInput = "p4Vertical";
                chargeInput = "p4Charge";
                break;
            default:
                horizontalInput = "p1Horizontal";
                verticalInput = "p1Vertical";
                chargeInput = "p1Charge";
                break;
        }
        gunScript.LoadControls(player);
    }

    public void UIIinit(GameObject newui) // Called when a new UI needs to be assigned (piloting new vehicle)
    {
        UI = newui;
        UI.transform.GetChild(0).gameObject.SetActive(true);
        UI.transform.GetChild(1).gameObject.SetActive(true);
        ChargeBar = UI.transform.GetChild(0).gameObject;
        ChargeBarFill = ChargeBar.transform.GetChild(0).gameObject;
        HealthBar = UI.transform.GetChild(1).gameObject;
        HealthBarFill = HealthBar.transform.GetChild(0).gameObject;
    }

    void Turn(float direction, float bTurn, float mTurn) //direction is a value between -1 and 1
    {
        body.AddTorque(Vector3.up * (bTurn + (TurnMultiplier * mTurn)) * direction);
    }

    void Accelerate(float bAcceleration, float mAcceleration, float bTopSpeed, float mTopSpeed)
    {
        body.AddForce(rotationModel.transform.forward * (bAcceleration + (AccelerationMultiplier * mAcceleration)));
        HorizontalSpeedCheck(bTopSpeed, mTopSpeed);
    }

    void Boost(float charge, float bBoost, float mBoost)
    {
        if(charge >= 1)
        {
            body.AddForce(transform.forward * 50 * (bBoost + (BoostMultiplier * mBoost)));
        }
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 0.2f; // lower sideways speed
        localVelocity.z = body.velocity.magnitude;
        body.velocity = body.transform.TransformDirection(localVelocity);
    }

    void Charge(float bArmor, float mArmor, float bBoost, float mBoost, float bTopSpeed, float mTopSpeed)
    {
        float weightmult = 0.003f * (bArmor + (ArmorMultiplier * mArmor)) * (usesDrag ? 1 : 0 );
        body.velocity = new Vector3 ( body.velocity.x * (1f - weightmult), (flying ? -20f : body.velocity.y), body.velocity.z * (1f - weightmult) );
        currentCharge += 0.0015f * (bBoost + (BoostMultiplier * mBoost));
        // Top Speed failsafe (slipcell, etc)
        if (body.velocity.magnitude > (bTopSpeed + (TopSpeedMultiplier * mTopSpeed)) * flightSpeedMultiplier)
        {
            body.velocity *= .96f;
        }
    }

    void HorizontalSpeedCheck(float bTopSpeed, float mTopSpeed)
    {
        if (Mathf.Abs(body.velocity.x) + Mathf.Abs(body.velocity.z) > (bTopSpeed + (TopSpeedMultiplier * mTopSpeed)) * flightSpeedMultiplier)
        {
            body.velocity = new Vector3(body.velocity.x * 0.97f, body.velocity.y, body.velocity.z * 0.97f);
        }
    }

    void DoDrag(float bArmor, float mArmor)
    {
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 1f - (0.001f*(bArmor + (ArmorMultiplier * mArmor))); // lower sideways speed
        body.velocity =  body.transform.TransformDirection(localVelocity);
    }

    void GroundAlign(float bAir, float mAir) // Merged with old CheckFlight code
    {
        if(!flying)
        {
            var ray = Physics.Raycast(transform.position, Vector3.up * -1f, out RaycastHit rayhit, 1.5f, LayerMask.GetMask("Environment"), QueryTriggerInteraction.Ignore);
            //Debug.Log(Vector3.Angle(rayhit.normal, Vector3.up));
            if (ray && Vector3.Angle(rayhit.normal, Vector3.up) < 45f)
            {
                transform.position = rayhit.point + new Vector3(0, 0.6f, 0);
                rotationModel.transform.up -= (rotationModel.transform.up - rayhit.normal) * 0.1f;
                rotationModel.transform.Rotate(transform.rotation.eulerAngles);
            }
            else
            {
                Debug.Log(ray);
                flying = true;
                stableFlight = true;
                body.AddForce((transform.up + transform.forward) * (bAir + (AirMultiplier * mAir)) / 4, ForceMode.Impulse);
                flightTimer = 0.5f * (bAir + (AirMultiplier * mAir));
                totalFlightTime = flightTimer;
            }
        }
        else
        {
            rotationModel.transform.rotation = (transform.rotation);
        }
    }

    void Aim(float direction, float bAir, float mAir)
    {
        float rotationUpperBound = -45;
        float rotationLowerBound = 45;
        Vector3 currentRotation = transform.localRotation.eulerAngles;

        float NR = GetNormalizedRotation();

        if ((direction < 0 && NR > rotationUpperBound) || (direction > 0 && NR < rotationLowerBound))
        {
            body.AddRelativeTorque(Vector3.right * direction * 5);
        }
        
        if (NR < 0) // vehicle is pointing up, NR is negative
        {
            flightSpeedMultiplier = 1.3f - (Mathf.Abs(NR / 45) * 0.4f + (bAir + (AirMultiplier * mAir))/10);
        }
        else if (NR > 0) // vehicle is pointing down, NR is positive
        {
            flightSpeedMultiplier = 1.3f + (Mathf.Abs(NR / 45) * 1.7f + (bAir + (AirMultiplier * mAir))/10);
        }
        else
        {
            flightSpeedMultiplier = 1.3f + (bAir + (AirMultiplier * mAir)) / 10;
        }

        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.y *= 0.97f; // dampen vertical speed
        body.velocity = body.transform.TransformDirection(localVelocity);
    }

    void DoFlightGravity()
    {
        flightTimer -= Time.deltaTime;

        float NR = GetNormalizedRotation();

        if(!stableFlight)
        {
            body.AddForce(5 * Vector3.up * (flightTimer / totalFlightTime));
        }

        if(flightTimer <= 0 && stableFlight == true)
        {
            stableFlight = false;
        }
    }

    void Eject()
    {
        flying = false;
        flightSpeedMultiplier = 1f;
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

        GameObject newplayerobject = Instantiate(playerCharacter, transform.position + transform.up*0.5f, transform.rotation);
        newplayerobject.GetComponent<PlayerCharacter>().LoadControls(player);
        player = 0;

        newplayerobject.GetComponent<PlayerCharacter>().cam = cam;
        cam.GetComponent<CamFollow>().target = newplayerobject;
        cam.GetComponent<CamFollow>().targetTransform = newplayerobject.transform;
        cam.GetComponent<CamFollow>().mode = "Player";
        cam = null;

        newplayerobject.GetComponent<PlayerCharacter>().UI = UI;
        newplayerobject.GetComponent<Rigidbody>().AddForce(transform.forward * -0.02f + transform.up * 0.05f, ForceMode.Impulse);
        UI = null;
        ChargeBar.SetActive(false);
        ChargeBar = null;
        ChargeBarFill = null;
        HealthBar.SetActive(false);
        HealthBar = null;
        HealthBarFill = null;
    }

    public void doMoveAlongCurve(Vector3 startpoint, Vector3 endpoint)
    {
        bpadstart = startpoint;
        bpadend = endpoint;
        StartCoroutine("MoveAlongCurve");
    }

    IEnumerator MoveAlongCurve()
    {
        GetComponent<Rigidbody>().useGravity = false;
        int oldplayer = player;
        player = 0;
        Vector3 middlepoint = new Vector3((bpadstart.x + bpadend.x) / 2, Mathf.Max(bpadstart.y, bpadend.y) + 10f, (bpadstart.z + bpadend.z) / 2);
        float timer = 0;
        while(timer < 1.5f)
        {
            Vector3 m1 = Vector3.Lerp(bpadstart, middlepoint, timer/1.5f);
            Vector3 m2 = Vector3.Lerp(middlepoint, bpadend, timer / 1.5f);
            transform.position = Vector3.Lerp(m1, m2, timer / 1.5f);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        GetComponent<Rigidbody>().useGravity = true;
        player = oldplayer;
    }

    private float GetNormalizedRotation()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;

        float normalizedRotation;
        if (currentRotation.x > 180)
        {
            normalizedRotation = -360 + currentRotation.x;
        }
        else
        {
            normalizedRotation = currentRotation.x;
        }
        return normalizedRotation;
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
        else if (item.GetComponent<BaseItem>().itemType == "Part Pickup")
        {
            partScript.PickupPart(item);
        }
        Destroy(item);
    }

    void takeForceHit(float force, float damage, Vector3 forcedirection)
    {
        body.AddForce(forcedirection * force * 10);
        curHP -= damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.GetComponent<BaseItem>() != null) // trigger collision is an item
        {
            if(collidedObject.GetComponent<BaseItem>().pickupTimer <= 0)
            {
                PickupItem(collidedObject);
            }
        }
        if (collidedObject.GetComponent<BasicProjectile>() != null && collidedObject.GetComponent<BasicProjectile>().owner != transform) // trigger collision is an enemy projectile
        {
            takeForceHit(collidedObject.GetComponent<BasicProjectile>().force, collidedObject.GetComponent<BasicProjectile>().damage, collidedObject.transform.forward);
            collidedObject.GetComponent<BasicProjectile>().Detonate();
        }
        if (collidedObject.GetComponent<BombProjectileExplosion>() != null && collidedObject.GetComponent<BombProjectileExplosion>().owner != transform) // trigger collision is an enemy explosion
        {
            takeForceHit(collidedObject.GetComponent<BombProjectileExplosion>().force, collidedObject.GetComponent<BombProjectileExplosion>().damage, Vector3.Normalize(transform.position - collidedObject.transform.position));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(flying && collidedObject.tag == "Environment")
        {
            var ray = Physics.Raycast(transform.position, Vector3.up * -1f, out RaycastHit rayhit, 0.7f, LayerMask.GetMask("Environment"), QueryTriggerInteraction.Ignore);
            if (ray && Vector3.Angle(rayhit.normal, Vector3.up) < 45f)
            {
                flying = false;
                flightSpeedMultiplier = 1f;
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
                body.angularVelocity = Vector3.zero;
            }
        }
    }
}
