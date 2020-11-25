using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Sprite one;
    public Sprite two;
    public Sprite three;
    private int curnum;

    public void doCount()
    {
        StartCoroutine("Count");
    }

    IEnumerator Count()
    {
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
        GetComponent<Image>().sprite = three;
        curnum = 3;
        while(curnum != 0)
        {
            float timer = 0;
            while (timer < 0.35f)
            {
                GetComponent<RectTransform>().localScale = Vector3.Lerp(new Vector3(2, 2, 1), new Vector3(1, 1, 1), timer / 0.35f);
                GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), timer / 0.35f);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            while (timer < 0.15f)
            {
                GetComponent<RectTransform>().localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0.5f, 0.5f, 1), timer / 0.15f);
                GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), timer / 0.15f);
                timer += Time.deltaTime;
                yield return null;
            }
            switch(curnum)
            {
                case 3:
                    curnum = 2;
                    GetComponent<Image>().sprite = two;
                    break;
                case 2:
                    curnum = 1;
                    GetComponent<Image>().sprite = one;
                    break;
                case 1:
                    curnum = 0;
                    break;
                default:
                    Debug.Log("Invalid Countdown number");
                    break;
            }
        }
        yield return null;
    }
}
