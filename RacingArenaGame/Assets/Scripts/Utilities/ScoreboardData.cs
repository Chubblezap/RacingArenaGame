using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardData : MonoBehaviour
{
    public GameObject leadingPlayer;
    public Player[] playerOrder;
    public string[] playerScoreString;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
