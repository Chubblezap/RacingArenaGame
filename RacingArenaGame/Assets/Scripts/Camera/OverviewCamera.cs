using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewCamera : MonoBehaviour
{
    private Vector3 motionstart;
    private Vector3 motiondestination;
    private Vector3 motionmidpoint; // used in curves
    private Vector3 camfocus;
    private float motiontimer;

    // Start is called before the first frame update
    void Start()
    {
        //doSweep(new Vector3(6, 40, 285), new Vector3(6, 40, -130), Quaternion.Euler(45, 180, 0), 6);
        //doPushPull(new Vector3(-36, 47, 60), -100, Quaternion.Euler(35, 110, 0), 2);
        //doMoveAlongCurve(new Vector3(70, 60, 180), new Vector3(150, 60, 45), new Vector3(70, 60, -90), new Vector3(0, 0, 25), 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void doSweep(Vector3 startpos, Vector3 endpos, Quaternion rot, float time) // Move from point A to B at a set rotation over a specified time
    {
        transform.position = startpos;
        transform.rotation = rot;
        motionstart = startpos;
        motiondestination = endpos;
        motiontimer = time;
        StartCoroutine("Sweep");
    }

    IEnumerator Sweep()
    {
        float timer = 0;
        while (timer < motiontimer)
        {
            transform.position = new Vector3(Mathf.SmoothStep(motionstart.x, motiondestination.x, (timer / motiontimer)), Mathf.SmoothStep(motionstart.y, motiondestination.y, (timer / motiontimer)), Mathf.SmoothStep(motionstart.z, motiondestination.z, (timer / motiontimer)));
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    void doPushPull(Vector3 startpos, float multiplier, Quaternion rot, float time) // Move from point A to point A + foward/backward at a set rotation over a specified time
    {
        transform.position = startpos;
        transform.rotation = rot;
        motionstart = startpos;
        motiondestination = startpos + transform.forward * multiplier;
        motiontimer = time;
        StartCoroutine("Sweep");
    }

    public void doMoveAlongCurve(Vector3 startpoint, Vector3 midpoint, Vector3 endpoint, Vector3 focuspoint, float time)
    {
        transform.position = startpoint;
        motionstart = startpoint;
        motionmidpoint = midpoint;
        motiondestination = endpoint;
        motiontimer = time;
        camfocus = focuspoint;
        StartCoroutine("MoveAlongCurve");
    }

    IEnumerator MoveAlongCurve()
    {
        float timer = 0;
        while (timer < motiontimer)
        {
            Vector3 m1 = new Vector3(Mathf.SmoothStep(motionstart.x, motionmidpoint.x, (timer / motiontimer)), Mathf.SmoothStep(motionstart.y, motionmidpoint.y, (timer / motiontimer)), Mathf.SmoothStep(motionstart.z, motionmidpoint.z, (timer / motiontimer)));
            Vector3 m2 = new Vector3(Mathf.SmoothStep(motionmidpoint.x, motiondestination.x, (timer / motiontimer)), Mathf.SmoothStep(motionmidpoint.y, motiondestination.y, (timer / motiontimer)), Mathf.SmoothStep(motionmidpoint.z, motiondestination.z, (timer / motiontimer)));
            transform.position = new Vector3(Mathf.SmoothStep(m1.x, m2.x, (timer / motiontimer)), Mathf.SmoothStep(m1.y, m2.y, (timer / motiontimer)), Mathf.SmoothStep(m1.z, m2.z, (timer / motiontimer)));
            transform.LookAt(camfocus);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
