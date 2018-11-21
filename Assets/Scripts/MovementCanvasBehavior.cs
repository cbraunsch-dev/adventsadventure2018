using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCanvasBehavior : MonoBehaviour {

	private static MovementCanvasBehavior instance = null;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
	}
}
