using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Renderer rend;
    AudioSource src;
    Color[] colorArray;
    private int currentColor;
    private bool amIActivated = false;
    // Use this for initialization
    void Start()
    {
        colorArray = new Color[8];
        rend = GetComponent<Renderer>();
        src = GetComponent<AudioSource>();
        rend.material.color = Color.red;
        currentColor = BallArrayScript.CurrentColorIndex;
        colorArray[0] = Color.red;
        colorArray[1] = Color.green;
        colorArray[2] = Color.blue;
        colorArray[3] = Color.magenta;
        colorArray[4] = Color.yellow;
        colorArray[5] = Color.cyan;
        colorArray[6] = Color.grey;
        colorArray[7] = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            BallArrayScript.CurrentColorIndex = 1;
            currentColor = BallArrayScript.CurrentColorIndex - 1;
            BallArrayScript.NumberOfBallsActivated = 0;
            amIActivated = false;
            rend.material.color = colorArray[currentColor];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentColor != BallArrayScript.CurrentColorIndex)
        {
            amIActivated = false;
        }

        if (!amIActivated)
        {
            if (other.gameObject.name.Equals("RightHand") || other.gameObject.name.Equals("LeftHand"))
            {
                //Debug.Log("Current ball touched: " + BallArrayScript.NumberOfBallsActivated);
                //Debug.Log("Color index: " + BallArrayScript.CurrentColorIndex);
                rend.material.color = colorArray[BallArrayScript.CurrentColorIndex];
                currentColor = BallArrayScript.CurrentColorIndex;
                amIActivated = true;
                BallArrayScript.NumberOfBallsActivated++;
                src.Play();
            }
        }

    }
}
