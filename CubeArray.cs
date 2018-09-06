using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeArray : MonoBehaviour
{
    private Transform[] cubeArray;
    private Cube[] cube;
    private int childCount;
	// Use this for initialization
	void Start ()
    {
        childCount = transform.childCount;
        cubeArray = new Transform[transform.childCount];
        cube = new Cube[childCount];
		for (int i = 0; i < childCount; i++)
        {
            cubeArray[i] = transform.GetChild(i);
            cube[i] = cubeArray[i].GetComponent<Cube>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetAll();
        }
	}

    public void ResetAll()
    {
        for (int i = 0; i < childCount; i++)
        {
            cube[i].ResetPosition();
        }
    }
}
