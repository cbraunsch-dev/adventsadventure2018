using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    public GameObject startingSpace;
    public float speed = 5;

    private GameObject currentSpace;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        this.currentSpace = startingSpace;
        SetPositionAsTarget(this.currentSpace);
    }
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("d")) {
            MoveToNextSpace(this.currentSpace);
        } else if (Input.GetKeyDown("a")) {
            MoveToPreviousSpace(this.currentSpace);
        }

        //Move towards target
        MoveTowards(targetPosition);
	}

    void MoveToNextSpace(GameObject space) {
        var nextSpace = space.GetComponent<SpaceBehavior>().nextSpace;
        if (nextSpace != null) {
            this.currentSpace = nextSpace;
            SetPositionAsTarget(this.currentSpace);
        }
    }

    void MoveToPreviousSpace(GameObject space) {
        var previousSpace = space.GetComponent<SpaceBehavior>().previousSpace;
		if (previousSpace != null)
		{
			this.currentSpace = previousSpace;
            SetPositionAsTarget(this.currentSpace);
		}
    }

    void SetPositionAsTarget(GameObject space) {
        //Lock the y-axis so that the player remains at a constant height while moving. The game board will be flat
        this.targetPosition = new Vector3(space.transform.position.x, this.transform.position.y, space.transform.position.z);
    }

    void MoveTowards(Vector3 position) {
		//Move player between game spaces
		// The step size is equal to speed times frame time.
		float step = speed * Time.deltaTime;

		// Move our position a step closer to the target
        transform.position = Vector3.MoveTowards(transform.position, position, step);
    }
}
