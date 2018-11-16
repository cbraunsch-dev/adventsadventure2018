using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorLeftSideBehavior : MonoBehaviour {
	private MechanicManagerBehavior mechanicManager;

	// Use this for initialization
	void Start()
	{
		this.mechanicManager = GameObject.FindWithTag(Tags.MechanicManager).GetComponent<MechanicManagerBehavior>();
	}

	void OnTriggerExit(Collider other)
	{
        this.mechanicManager.DidEnterVisor();
	}
}
