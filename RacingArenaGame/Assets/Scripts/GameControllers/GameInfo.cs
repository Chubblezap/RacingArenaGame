using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int[] players = new int[4];
    public int timeMinutes;
    public int timeSeconds;
    public int leadingPlayer; // The player that controls pausing, scene transitions, etc

    public void Awake()
    {
    	DontDestroyOnLoad(gameObject);
    }
}
