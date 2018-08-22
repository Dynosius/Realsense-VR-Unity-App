using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
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
        if (other.gameObject.name.Equals("RightHand") || other.gameObject.name.Equals("LeftHand"))
        {
            rend.material.color = Color.green;
        }
    }
}
