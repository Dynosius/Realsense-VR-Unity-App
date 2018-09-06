using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class HitCubes : MonoBehaviour {
    private bool isAlreadyRunning = false;
    private bool resetFlag = false;
    public static Timer aTimer = new Timer();
    private CubeArray cubeArray;
	// Use this for initialization
	void Start ()
    {
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Interval = 5000;
        aTimer.Enabled = true;
        cubeArray = GameObject.Find("CubeArray").GetComponent<CubeArray>();
	}

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {

        //Cannot call method for resetting cubes from here because unity doesn't allow separate threads from changing gameobject transforms
        resetFlag = true;
        isAlreadyRunning = false;
        aTimer.Stop();
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update ()
    {
        if(resetFlag)
        {
            cubeArray.ResetAll();
            resetFlag = false;
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Cube"))
        {
            if(!isAlreadyRunning)
            {
                aTimer.Start();
            }
            else
            {
                isAlreadyRunning = true;
            }
        }
    }
}
