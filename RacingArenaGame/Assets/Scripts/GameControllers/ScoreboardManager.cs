using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    private ScoreboardData scoredata;
    public GameObject scoresheet;

    // Start is called before the first frame update
    void Start()
    {
        scoredata = GameObject.Find("ScoreboardData(Clone)").GetComponent<ScoreboardData>();
        AddScores(scoredata);
        GameObject.Find("Fader").GetComponent<Fader>().doFadeOut(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddScores(ScoreboardData s)
    {
        for(int i=0; i < s.playerOrder.Length; i++)
        {
            scoresheet.transform.GetChild(i).GetComponent<ScoreboardItem>().SetScore(s.playerOrder[i].playerNum, s.playerScoreString[i]);
        }
    }
}
