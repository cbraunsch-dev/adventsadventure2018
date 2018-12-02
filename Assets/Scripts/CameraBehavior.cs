using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;
    private int zoomOffset = 5;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}

    void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            ZoomIn();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            ZoomOut();
        }
    }

    private void ZoomOut() {
        offset.x += zoomOffset;
        offset.y += zoomOffset;
        offset.z += zoomOffset;
    }

    private void ZoomIn() {
		offset.x -= zoomOffset;
		offset.y -= zoomOffset;
		offset.z -= zoomOffset;
    }
}
