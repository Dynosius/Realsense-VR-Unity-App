using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{

    private GrabScript parentScript;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("CupObject"))
        {
            GrabScript.SourceOfContact = this.gameObject.name;
        }
    }
}
