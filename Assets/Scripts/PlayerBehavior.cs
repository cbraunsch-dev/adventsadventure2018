using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("d")) {
            Debug.Log("Move to next field");
        } else if (Input.GetKeyDown("a")) {
            Debug.Log("Move to previous field");
        }
	}
}
