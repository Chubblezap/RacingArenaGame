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
    [HideInInspector]
    public GameObject WeaponBarL;
    [HideInInspector]
    public GameObject WeaponBarR;
    private GameObject Speedometer;

    // Stat modifiers
    [HideInInspector]
    public float curHP;
    public float camsize = 1; // for camera
    public float gunhoverdist = 1; //for held guns
    private float TopSpeedMultiplier, AccelerationMultiplier, TurnMultiplier, BoostMultiplier, ArmorMultiplier, OffenseMultiplier, AirMultiplier;
    [HideInInspector]
    public float boostPower = 0;

    // Utilities
    private GameObject gameMaster;
    private float isHolding;
    private float currentCharge;
    //private Collider myCollider;
    Rigidbody body;
    private GunHandler gunScript;
    private PartHandler partScript;
    private float ejectTimer;
    public Player myPlayer;
    public GameObject playerCharacter;
    public GameObject cam;
    public GameObject rotationModel;

    // Flags
    public bool usesDrag = true; // Slipcell
    public bool grounded = false;
    public bool halfBoosts = false; // Megabooster
    
    // flight
    [HideInInspector]
    public bool flying = false;
    private bool stableFlight = true;
    private float totalFlightTime;
    private float flightTimer;
    private float flightSpeedMultiplier;

    // bouncepad/rail
    public bool hasControl = true;
    [HideInInspector]
    public Vector3 bpadstart;
    [HideInInspector]
    public Vector3 bpadend;

    // Controls
    [HideInInspector]
    public float turnAmount;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayer != null && hasControl)
        {
            isHolding = Input.GetAxis(myPlayer.chargeInput);
            ChargeBarFill.GetComponent<Image>().fillAmount = currentCharge;
            HealthBarFill.GetComponent<Image>().fillAmount = curHP/MaxHP;
            if(WeaponBarL.activeSelf)
            {
                WeaponBarL.transform.GetChild(0).GetComponent<Image>().fillAmount = GetComponent<GunHandler>().leftGun.GetComponent<FiringHandler>().GetBarAmount();
            }
            if (WeaponBarR.activeSelf)
            {
                WeaponBarR.transform.GetChild(0).GetComponent<Image>().fillAmount = GetComponent<GunHandler>().rightGun.GetComponent<FiringHandler>().GetBarAmount();
            }
        }
    }

    void FixedUpdate() //put movement/physics stuff here
    {
        GroundAlign();
        if (myPlayer != null && hasControl)
        {
            turnAmount = Input.GetAxis(myPlayer.horizontalInput);
            Turn(turnAmount, BaseTurn, myPlayer.Turn);
            if (isHolding == 1) // player is holding Space or A
            {
                Charge(BaseArmor, myPlayer.Armor, BaseBoost, myPlayer.Boost, BaseTopSpeed, myPlayer.TopSpeed);
                if (Input.GetAxis(myPlayer.verticalInput) <= -0.75)
                {
                    ejectTimer += Time.deltaTime;
                    if (ejectTimer >= 1)
                    {
                        currentCharge = 0;
                        Eject();
                        return;
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
                    AlignVelocity();
                    StartCoroutine("Boost");
                }
                ejectTimer = 0;
                currentCharge = 0;
                Accelerate(BaseAcceleration, myPlayer.Acceleration, BaseTopSpeed, myPlayer.TopSpeed);
            }
            if(usesDrag)
            {
                DoDrag(BaseArmor, myPlayer.Armor);
            }
            if (flying)
            {
                Aim(Input.GetAxis(myPlayer.verticalInput), BaseAir, myPlayer.Air);
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
            HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(30f, MaxHP * 5f);
            HealthBarFill.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, MaxHP * 5f - 10f);
            WeaponBarL = UI.transform.GetChild(2).gameObject;
            WeaponBarL.SetActive(false);
            WeaponBarR = UI.transform.GetChild(3).gameObject;
            WeaponBarR.SetActive(false);
        }
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
        //myCollider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        gunScript = GetComponent<GunHandler>();
        partScript = GetComponent<PartHandler>();
        //LoadControls(startplayer);
    }

    /*public void LoadControls(int newplayernum)
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
    }*/

    public void UIIinit(GameObject newui) // Called when a new UI needs to be assigned (piloting new vehicle)
    {
        UI = newui;
        UI.transform.GetChild(0).gameObject.SetActive(true);
        UI.transform.GetChild(1).gameObject.SetActive(true);
        ChargeBar = UI.transform.GetChild(0).gameObject;
        ChargeBarFill = ChargeBar.transform.GetChild(0).gameObject;
        HealthBar = UI.transform.GetChild(1).gameObject;
        HealthBarFill = HealthBar.transform.GetChild(0).gameObject;
        WeaponBarL = UI.transform.GetChild(2).gameObject;
        if(GetComponent<GunHandler>().leftGun != null && (GetComponent<GunHandler>().leftGun.GetComponent<FiringHandler>().hasAmmo || GetComponent<GunHandler>().leftGun.GetComponent<FiringHandler>().hasCharge))
        {
            WeaponBarL.SetActive(true);
        }
        WeaponBarR = UI.transform.GetChild(3).gameObject;
        if (GetComponent<GunHandler>().rightGun != null && (GetComponent<GunHandler>().rightGun.GetComponent<FiringHandler>().hasAmmo || GetComponent<GunHandler>().rightGun.GetComponent<FiringHandler>().hasCharge))
        {
            WeaponBarR.SetActive(true);
        }
    }

    public void UIReload() // Check for new UI elements (equipping weapons, etc)
    {
        WeaponBarL = UI.transform.GetChild(2).gameObject;
        if (GetComponent<GunHandler>().leftGun != null && (GetComponent<GunHandler>().leftGun.GetComponent<FiringHandler>().hasAmmo || GetComponent<GunHandler>().leftGun.GetComponent<FiringHandler>().hasCharge))
        {
            WeaponBarL.SetActive(true);
        }
        WeaponBarR = UI.transform.GetChild(3).gameObject;
        if (GetComponent<GunHandler>().rightGun != null && (GetComponent<GunHandler>().rightGun.GetComponent<FiringHandler>().hasAmmo || GetComponent<GunHandler>().rightGun.GetComponent<FiringHandler>().hasCharge))
        {
            WeaponBarR.SetActive(true);
        }
    }

    void Turn(float direction, float bTurn, float mTurn) //direction is a value between -1 and 1
    {
        body.AddTorque(Vector3.up * (bTurn + (TurnMultiplier * mTurn)) * direction);
    }

    void Accelerate(float bAcceleration, float mAcceleration, float bTopSpeed, float mTopSpeed)
    {
        body.AddForce(rotationModel.transform.forward * (bAcceleration + (AccelerationMultiplier * mAcceleration) + boostPower));
        HorizontalSpeedCheck(bTopSpeed, mTopSpeed);
    }

    void AlignVelocity()
    {
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 0.2f; // lower sideways speed
        localVelocity.z = body.velocity.magnitude;
        body.velocity = body.transform.TransformDirection(localVelocity);
    }

    private IEnumerator Boost()
    {
        float boostTime = 0;
        float curBoostTime = 0;
        float MaxBoostPower = 0;

        if (currentCharge >= 1 || halfBoosts)
        {
            body.AddForce(rotationModel.transform.forward * (BaseBoost + (BoostMultiplier * myPlayer.Boost)) * 25);
            MaxBoostPower = BaseBoost + (BoostMultiplier * myPlayer.Boost) * 2;
            boostPower = MaxBoostPower;
            boostTime = 0.5f + (BaseBoost + (BoostMultiplier * myPlayer.Boost)) / 10;
            curBoostTime = boostTime;
        }
        while (curBoostTime > 0)
        {
            curBoostTime -= Time.deltaTime;
            if(isHolding == 1) // Cut boost if the player is braking
            {
                curBoostTime = 0;
            }
            boostPower = MaxBoostPower * (curBoostTime / boostTime);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
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
        if (Mathf.Abs(body.velocity.x) + Mathf.Abs(body.velocity.z) > (bTopSpeed + (TopSpeedMultiplier * mTopSpeed) + boostPower) * flightSpeedMultiplier)
        {
            body.velocity = new Vector3(body.velocity.x * 0.97f, body.velocity.y, body.velocity.z * 0.97f);
        }
    }

    void DoDrag(float bArmor, float mArmor)
    {
        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);
        localVelocity.x *= 1f - (0.001f*(bArmor + (ArmorMultiplier * mArmor))) - (grounded ? 0.7f : 0f); // lower sideways speed
        body.velocity =  body.transform.TransformDirection(localVelocity);
    }

    void GroundAlign() // Merged with old CheckFlight code
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
                if(myPlayer != null && hasControl)
                {
                    Launch(BaseAir, myPlayer.Air);
                }
            }
        }
        else
        {
            rotationModel.transform.rotation = (transform.rotation);
        }
    }

    void Launch(float bAir, float mAir)
    {
        flying = true;
        stableFlight = true;
        body.AddForce((transform.up + transform.forward) * (bAir + (AirMultiplier * mAir)) / 4, ForceMode.Impulse);
        flightTimer = 0.5f * (bAir + (AirMultiplier * mAir));
        totalFlightTime = flightTimer;
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
        newplayerobject.GetComponent<PlayerCharacter>().myPlayer = myPlayer;
        myPlayer = null;

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
        WeaponBarL.SetActive(false);
        WeaponBarL = null;
        WeaponBarR.SetActive(false);
        WeaponBarR = null;
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
        hasControl = false;
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
        hasControl = true;
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
                    if (myPlayer.TopSpeed < 18) { myPlayer.TopSpeed += 1; }
                    break;
                case "Acceleration":
                    if(myPlayer.Acceleration < 18) { myPlayer.Acceleration += 1; }
                    break;
                case "Turn":
                    if (myPlayer.Turn < 18) { myPlayer.Turn += 1; }
                    break;
                case "Boost":
                    if (myPlayer.Boost < 18) { myPlayer.Boost += 1; }
                    break;
                case "Armor":
                    if (myPlayer.Armor < 18) { myPlayer.Armor += 1; }
                    break;
                case "Offense":
                    if (myPlayer.Offense < 18) { myPlayer.Offense += 1; }
                    break;
                case "Air":
                    if (myPlayer.Air < 18) { myPlayer.Air += 1; }
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
        if(collidedObject.GetComponent<BaseItem>() != null && collidedObject.layer != 11) // trigger collision is an item and NOT in the crate layer
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
