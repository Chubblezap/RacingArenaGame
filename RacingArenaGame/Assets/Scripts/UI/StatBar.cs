using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public string stat;
    public Player target;

    // Update is called once per frame
    void Update()
    {
        if(stat != null && target != null)
        {
            if(stat == "Top Speed")
            {
                GetComponent<Image>().fillAmount = (float)target.TopSpeed / 18;
            }
            else if (stat == "Acceleration")
            {
                GetComponent<Image>().fillAmount = (float)target.Acceleration / 18;
            }
            else if (stat == "Boost")
            {
                GetComponent<Image>().fillAmount = (float)target.Boost / 18;
            }
            else if (stat == "Turn")
            {
                GetComponent<Image>().fillAmount = (float)target.Turn / 18;
            }
            else if (stat == "Armor")
            {
                GetComponent<Image>().fillAmount = (float)target.Armor / 18;
            }
            else if (stat == "Offense")
            {
                GetComponent<Image>().fillAmount = (float)target.Offense / 18;
            }
            else if (stat == "Air")
            {
                GetComponent<Image>().fillAmount = (float)target.Air / 18;
            }
            else
            {
                Debug.Log("Invalid or null stat assignment (UI)");
            }
        }
    }
}
