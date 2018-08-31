using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Renderer rend;
    AudioSource src;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        src = GetComponent<AudioSource>();
        rend.material.color = Color.red;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown("r"))
        {
            rend.material.color = Color.red;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(rend.material.color == Color.red)
        {
            if (other.gameObject.name.Equals("RightHand") || other.gameObject.name.Equals("LeftHand"))
            {
                rend.material.color = Color.green;
                src.Play();
            }
        }
        
    }
}
