using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    public GameObject upItem;
    public GameObject downItem;
    public GameObject leftItem;
    public GameObject rightItem;
    public GameObject pushItem;
    public GameObject backItem;
    public float pushCooldown = 0f;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pushCooldown > 0)
        {
            pushCooldown -= Time.deltaTime;
        }

        if(active && pushCooldown <= 0)
        {
            if(upItem != null)
            {
                if(Input.GetAxis("p1menuVertical") >= 0.25)
                {
                    SwitchItems(upItem);
                }
            }
            if (downItem != null)
            {
                if(Input.GetAxis("p1menuVertical") <= -0.25)
                {
                    SwitchItems(downItem);
                }
            }
            if (leftItem != null)
            {
                if (Input.GetAxis("p1menuHorizontal") <= -0.25)
                {
                    SwitchItems(leftItem);
                }
            }
            if (rightItem != null)
            {
                if (Input.GetAxis("p1menuHorizontal") >= 0.25)
                {
                    SwitchItems(rightItem);
                }
            }

            if(pushItem != null)
            {
                if(Input.GetAxis("p1menuButton") >= 0.25)
                {
                    SwitchItems(pushItem);
                }
            }
            if (backItem != null)
            {
                if (Input.GetAxis("p1menuButton") <= -0.25)
                {
                    SwitchItems(backItem);
                }
            }
        }
    }

    void SwitchItems(GameObject newitem)
    {
        if(newitem.transform.parent.name != transform.parent.name)
        {
            transform.parent.GetComponent<MenuContainer>().SlideOut();
            newitem.transform.parent.GetComponent<MenuContainer>().SlideIn();
        }
        newitem.GetComponent<MenuItem>().Activate();
        if(GetComponent<MenuBlink>() != null)
        {
            GetComponent<MenuBlink>().Deactivate();
        }
        active = false;
    }

    public virtual void Activate()
    {
        if (GetComponent<MenuBlink>() != null)
        {
            GetComponent<MenuBlink>().Activate();
        }
        active = true;
        pushCooldown = 0.25f;
    }
}
