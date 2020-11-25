using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardItem : MonoBehaviour
{
    public void SetScore(int playernum, string scoretext)
    {
        transform.GetChild(0).GetComponent<Text>().text = "P" + playernum.ToString();
        transform.GetChild(1).GetComponent<Text>().text = scoretext;
    }
}
