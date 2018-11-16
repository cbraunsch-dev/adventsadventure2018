using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicManagerBehavior : MonoBehaviour {
    private bool insideVisor = false;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space") && insideVisor)
		{
            Debug.Log("Nice! Next level!");
        } else if (Input.GetKeyDown("space")) { 
            Debug.Log("Sorry! Try again!");
        }
	}

    public void DidEnterVisor() {
        this.insideVisor = true;
    }

    public void DidExitVisor() {
        this.insideVisor = false;
    }
}
