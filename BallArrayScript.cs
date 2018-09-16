using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArrayScript : MonoBehaviour {
    public static int NumberOfBallsActivated { get; set; }
    public static int CurrentColorIndex { get; set; }
    private int maxNumofBalls;
    // Use this for initialization
    void Start()
    {
        NumberOfBallsActivated = 0;
        CurrentColorIndex = 1;
        maxNumofBalls = this.transform.childCount;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (NumberOfBallsActivated >= maxNumofBalls)        // 38 trenutno je max
        {
            NumberOfBallsActivated = 0;
            CurrentColorIndex = (CurrentColorIndex + 1) % 8;
        }
	}
}
