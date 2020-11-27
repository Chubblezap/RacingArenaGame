using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardManager : MonoBehaviour
{
    private ScoreboardData scoredata;
    private bool quittable = false;
    public GameObject scoresheet;

    // Start is called before the first frame update
    void Start()
    {
        scoredata = GameObject.Find("ScoreboardData(Clone)").GetComponent<ScoreboardData>();
        AddScores(scoredata);
        GameObject.Find("Fader").GetComponent<Fader>().doFadeOut(0.5f);
        StartCoroutine("ScoreDrops");
    }

    // Update is called once per frame
    void Update()
    {
        if(quittable && Input.GetButtonDown(scoredata.leadingPlayer.GetComponent<Player>().startInput))
        {
            Mango();
            SceneManager.LoadScene("MainMenu");
        }
    }

    void AddScores(ScoreboardData s)
    {
        for(int i=0; i < s.playerOrder.Length; i++)
        {
            scoresheet.transform.GetChild(i).GetComponent<ScoreboardItem>().SetScore(s.playerOrder[i].playerNum, s.playerScoreString[i]);
        }
    }

    private IEnumerator ScoreDrops()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for(int i = 3; i >= 0; i--)
        {
            scoresheet.transform.GetChild(i).GetComponent<ScoreboardItem>().StartCoroutine("DropIn");
            yield return new WaitForSecondsRealtime(0.5f);
        }
        GameObject.Find("Fader").GetComponent<Fader>().doFadeIn(0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        GameObject.Find("Fader").GetComponent<Fader>().doFadeOut(3f);
        yield return new WaitForSecondsRealtime(3f);
        quittable = true;
        yield return null;
    }

    private void Mango() //blow up
    {
        Destroy(GameObject.Find("ScoreboardData(Clone)"));
        Destroy(GameObject.Find("DataCarrier(Clone)"));
    }
}
