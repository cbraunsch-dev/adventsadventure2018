using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorRightSideBehavior : MonoBehaviour {
    private MechanicManagerBehavior mechanicManager;

	// Use this for initialization
	void Start()
	{
		this.mechanicManager = GameObject.FindWithTag(Tags.MechanicManager).GetComponent<MechanicManagerBehavior>();
	}

	void OnTriggerEnter(Collider other)
	{
        this.mechanicManager.DidExitVisor();
	}
}
