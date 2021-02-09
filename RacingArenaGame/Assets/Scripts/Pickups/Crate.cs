using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : BaseItem
{
    public int numItems;
    public string crateType;
    public GameObject[] items;
    public float maxHP;
    public float curHP;
    public GameObject breakLayer;
    public Texture2D[] cracks;

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Crate";
        int crateDecider = Random.Range(1, 4);
        switch (crateDecider)
        {
            case 1:
                crateType = "Stats";
                numItems = Random.Range(1, 5);
                break;
            case 2:
                crateType = "Powers";
                numItems = Random.Range(1, 3);
                break;
            case 3:
                crateType = "Mechanical";
                numItems = Random.Range(1, 2);
                break;
            default:
                Debug.Log("weird crate");
                break;
        }
        maxHP = Random.Range(20, 50);
        curHP = maxHP;
    }

    public void DoHit(Vector3 force, float damage)
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up, ForceMode.Impulse);
        curHP -= damage;
        if(curHP <= 0)
        {
            Pop();
        }
        if(curHP > maxHP/2 && curHP <= maxHP*0.75)
        {
            breakLayer.GetComponent<MeshRenderer>().material.SetTexture("_AlphaTex", cracks[0]);
        }
        else if(curHP > maxHP/4 && curHP <= maxHP/2)
        {
            breakLayer.GetComponent<MeshRenderer>().material.SetTexture("_AlphaTex", cracks[1]);
        }
        else if (curHP > 0 && curHP <= maxHP/4)
        {
            breakLayer.GetComponent<MeshRenderer>().material.SetTexture("_AlphaTex", cracks[2]);
        }
    }

    void Pop()
    {
        for(int i = 0; i < numItems; i++)
        {
            GameObject newitem;
            switch (crateType)
            {
                case "Stats":
                    newitem = Instantiate(items[0], transform.position, Quaternion.identity);
                    break;
                case "Powers":
                    newitem = Instantiate(items[1], transform.position, Quaternion.identity);
                    break;
                case "Mechanical":
                    int tmp = Random.Range(2, 4);
                    newitem = Instantiate(items[tmp], transform.position, Quaternion.identity);
                    if (newitem.GetComponent<GunPickup>() != null) { newitem.GetComponent<GunPickup>().Init(); }
                    if (newitem.GetComponent<PartPickup>() != null) { newitem.GetComponent<PartPickup>().Init(); }
                    break;
                default:
                    Debug.Log("weird drops");
                    newitem = null;
                    break;
            }
            if(newitem != null)
            {
                newitem.GetComponent<Rigidbody>().AddForce(newitem.transform.up * 1 + newitem.transform.right * Random.Range(-1f, 1f) + newitem.transform.forward * Random.Range(-1f, 1f), ForceMode.Impulse);
            }
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.tag == "Projectile")
        {
            if(collidedObject.GetComponent<BasicProjectile>())
            {
                DoHit(collidedObject.GetComponent<BasicProjectile>().force * Vector3.Normalize(collidedObject.transform.forward), collidedObject.GetComponent<BasicProjectile>().damage);
                collidedObject.GetComponent<BasicProjectile>().Detonate();
            }
            else if(collidedObject.GetComponent<BombProjectileExplosion>())
            {
                DoHit(collidedObject.GetComponent<BombProjectileExplosion>().force * Vector3.Normalize(transform.position - collidedObject.transform.position), collidedObject.GetComponent<BombProjectileExplosion>().damage);
            }
        }
    }
}
