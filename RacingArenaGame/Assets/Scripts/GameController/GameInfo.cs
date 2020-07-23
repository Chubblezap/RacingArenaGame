using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int[] players = new int[4];
    public int timeMinutes;
    public int timeSeconds;

    public void Awake()
    {
    	DontDestroyOnLoad(gameObject);
    }
}
