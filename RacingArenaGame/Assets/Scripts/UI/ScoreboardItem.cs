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

    public void SetAlpha(float a) // a = 0-1
    {
        GetComponent<Image>().color = new Color(1, 1, 1, a);
        for(int i=0; i<transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Text>() != null)
            {
                transform.GetChild(i).GetComponent<Text>().color = new Color(0.2f, 0.2f, 0.2f, a);
            }
            else if (transform.GetChild(i).GetComponent<Image>() != null)
            {
                transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, a);
            }
        }
    }

    public IEnumerator DropIn()
    {
        float timer = 0;
        Vector3 initSize = new Vector3(1, 1, 1);
        Vector2 bigSize = new Vector3(3, 3, 1);
        while (timer < 0.5f)
        {
            GetComponent<RectTransform>().localScale = Vector3.Lerp(bigSize, initSize, timer / 0.5f);
            SetAlpha(timer * 2);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
