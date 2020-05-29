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

    // Start is called before the first frame update
    void Start()
    {
        itemType = "Crate";
        int crateDecider = Random.Range(1, 3);
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

    void DoHit(Vector3 force, float damage)
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up, ForceMode.Impulse);
        curHP -= damage;
        if(curHP <= 0)
        {
            Pop();
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
            DoHit(collidedObject.GetComponent<BasicProjectile>().force * Vector3.Normalize(collidedObject.transform.forward), collidedObject.GetComponent<BasicProjectile>().damage);
            collidedObject.GetComponent<BasicProjectile>().Detonate();
        }
    }
}
