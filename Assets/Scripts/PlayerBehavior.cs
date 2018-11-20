﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    public GameObject startingSpace;
    public float speed = 5;
    public Inventory Inventory { get; private set; }

    private GameObject currentSpace;
    private Vector3 targetPosition;
    private GameManagerBehavior gameManager;

    public void LoadInventory(Inventory inventory) {
        this.Inventory = inventory;
        this.PrintInventory();
    }

	// Use this for initialization
	void Start () {
        this.currentSpace = startingSpace;
        SetPositionAsTarget(this.currentSpace);
        if (this.Inventory == null)
        {
            this.Inventory = new Inventory();
        }
        this.gameManager = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
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

	void OnTriggerEnter(Collider other)
	{
        var spaceBehavior = other.gameObject.GetComponent<SpaceBehavior>();
        if (spaceBehavior != null)
        {
            if (!spaceBehavior.visited)
            {
                var spaceEvent = spaceBehavior.triggeredEvent;
                switch (spaceEvent)
                {
                    case SpaceEvent.earnMoney:
                        this.Inventory.CollectMoney(10);
                        break;
                    case SpaceEvent.loseMoney:
                        this.Inventory.SpendMoney(10);
                        break;
                    case SpaceEvent.visitStore:
                        var store = spaceBehavior.store.GetComponent<StoreBehavior>();
                        store.ShowMessage();
                        break;
                }
                spaceBehavior.visited = true;
                this.PrintInventory();
                this.gameManager.VisitedSpace(other.gameObject);
            }
        }
	}

    private void PrintInventory() {
        Debug.Log("Money: " + this.Inventory.Money + " Nr. of items: " + this.Inventory.Items.Count);
    }

    public void Buy(int itemIndex) {
        var item = ItemCreator.CreateItem(itemIndex);
        this.Inventory.BuyItem(item);
    }
}
