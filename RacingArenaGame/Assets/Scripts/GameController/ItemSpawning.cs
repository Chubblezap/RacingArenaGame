using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    // Prefab references
    public GameObject StatPickup;
    public GameObject PowerPickup;
    public GameObject PartPickup;
    public GameObject GunPickup;
    public GameObject Crate;

    // Utilities
    private GameObject boundsObject;
    private Collider[] itemSpawnBounds;
    private float itemSpawnTimer;
    private float timeToSpawn;
    private float gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        itemSpawnTimer += Time.deltaTime;
        if(itemSpawnTimer > timeToSpawn)
        {
            SpawnItem();
            itemSpawnTimer = 0;
            timeToSpawn = Random.Range(0f, 2f);
        }
    }

    void Init()
    {
        itemSpawnTimer = 0;
        gameTimer = 0;
        timeToSpawn = Random.Range(0f, 2f);
        boundsObject = GameObject.Find("ItemSpawnBounds");
        itemSpawnBounds = boundsObject.GetComponents<BoxCollider>();
    }

    void SpawnItem()
    {
        int boxDecider = Random.Range(0, itemSpawnBounds.Length);
        int itemDecider = Random.Range(1, 21);
        if(1 <= itemDecider && itemDecider < 11)
        {
            Instantiate(StatPickup, RandomPointInBounds(itemSpawnBounds[boxDecider].bounds), Quaternion.identity);
        }
        else if(11 <= itemDecider && itemDecider < 15)
        {
            Instantiate(PowerPickup, RandomPointInBounds(itemSpawnBounds[boxDecider].bounds), Quaternion.identity);
        }
        else if(15 <= itemDecider && itemDecider < 18)
        {
            Instantiate(PartPickup, RandomPointInBounds(itemSpawnBounds[boxDecider].bounds), Quaternion.identity);
        }
        else if (18 <= itemDecider && itemDecider < 20)
        {
            Instantiate(GunPickup, RandomPointInBounds(itemSpawnBounds[boxDecider].bounds), Quaternion.identity);
        }
        else if(itemDecider == 20)
        {
            Instantiate(Crate, RandomPointInBounds(itemSpawnBounds[boxDecider].bounds), Quaternion.identity);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
