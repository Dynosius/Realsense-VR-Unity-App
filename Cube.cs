using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    private Vector3 initialPosition, initialRotation;
	// Use this for initialization
	void Start ()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ResetPosition()
    {
        this.transform.rotation = Quaternion.Euler(initialRotation);
        this.transform.position = initialPosition;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
