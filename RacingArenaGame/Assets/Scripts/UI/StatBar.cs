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
                GetComponent<Image>().fillAmount = target.TopSpeed / 18;
            }
            else if (stat == "Acceleration")
            {
                GetComponent<Image>().fillAmount = target.Acceleration / 18;
            }
            else if (stat == "Boost")
            {
                GetComponent<Image>().fillAmount = target.Boost / 18;
            }
            else if (stat == "Turn")
            {
                GetComponent<Image>().fillAmount = target.Turn / 18;
            }
            else if (stat == "Armor")
            {
                GetComponent<Image>().fillAmount = target.Armor / 18;
            }
            else if (stat == "Offense")
            {
                GetComponent<Image>().fillAmount = target.Offense / 18;
            }
            else if (stat == "Air")
            {
                GetComponent<Image>().fillAmount = target.Air / 18;
            }
            else
            {
                Debug.Log("Invalid or null stat assignment (UI)");
            }
        }
    }
}
