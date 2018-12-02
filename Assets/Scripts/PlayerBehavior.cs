using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
    public GameObject startingSpace;
    public float speed = 5;
    public Inventory Inventory { get; private set; }
    public GameObject collectiblesText;
    public GameObject nrOfTurnsText;

    private GameObject currentSpace;
    private GameObject nextWayPoint;
    private Vector3 targetPosition;
    private GameManagerBehavior gameManager;
    private GameObject destination;

    public void LoadInventory(Inventory inventory) {
        this.Inventory = inventory;
        this.UpdateHUD();
    }

    public void PlacePlayerAtSpace(GameObject space) {
		startingSpace = space;
        currentSpace = space;
        this.SetInitialPositionAndDestination();
    }

    public void ScheduleMovement(int numberOfMoves) {
        this.destination = FindDestination(numberOfMoves, currentSpace);
        this.MoveToNextWayPoint();
    }

    private void MoveToNextWayPoint() {
        var nextSpace = currentSpace.GetComponent<SpaceBehavior>().nextSpace;
        this.nextWayPoint = nextSpace;
        SetPositionAsTarget(nextWayPoint);
    }

    private GameObject FindDestination(int numberOfMoves, GameObject space) {
        if (numberOfMoves > 0 && space.GetComponent<SpaceBehavior>().nextSpace != null) {
            return FindDestination(numberOfMoves - 1, space.GetComponent<SpaceBehavior>().nextSpace);
        }
        return space;
    }

	// Use this for initialization
	void Start ()
    {
        if(this.currentSpace == null) {
			this.currentSpace = startingSpace;
			SetInitialPositionAndDestination();    
        }
        if (this.Inventory == null)
        {
            this.Inventory = new Inventory();
        }
        this.gameManager = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
        this.UpdateHUD();
    }

    private void SetInitialPositionAndDestination()
    {
        SetPositionAsTarget(this.startingSpace);
        if (this.destination == null)
        {
            this.destination = startingSpace;
        }
    }

    // Update is called once per frame
    void Update () {
        MoveTowards(targetPosition);
	}

    void SetPositionAsTarget(GameObject space) {
        //Lock the y-axis so that the player remains at a constant height while moving. The game board will be flat
        this.targetPosition = new Vector3(space.transform.position.x, this.transform.position.y, space.transform.position.z);
        Debug.Log("Set target position to: " + targetPosition);
    }

    void MoveTowards(Vector3 position)
    {
        //Move player between game spaces
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        FacePlayerInDirectionOfMovement(position);
    }

    private void FacePlayerInDirectionOfMovement(Vector3 position)
    {
        var dist = Vector3.Distance(transform.position, position);
        var xDist = position.x - transform.position.x;
        var zDist = position.z - transform.position.z;
        if (xDist != 0)
        {
            var angle = Mathf.Atan(zDist / xDist);
            var angleDegrees = angle * Mathf.Rad2Deg;
            if (xDist < 0)
            {
                var finalAngle = 180 - angleDegrees;
                transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.up);
            }
            else
            {
                var finalAngle = 360 - angleDegrees;
                transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.up);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.destination)
        {
            this.currentSpace = other.gameObject;
            VisitSpace(other);
        }
        else if (other.gameObject == this.nextWayPoint)
        {
            this.currentSpace = other.gameObject;
            MoveToNextWayPoint();
        }
    }

    private void VisitSpace(Collider other)
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
                        this.FinishVisit(other.gameObject, 0);
                        break;
                    case SpaceEvent.loseMoney:
                        this.Inventory.SpendMoney(10);
                        this.FinishVisit(other.gameObject, 0);
                        break;
                    case SpaceEvent.visitStore:
                        var store = spaceBehavior.store.GetComponent<StoreBehavior>();
                        store.ShowMessage(this.gameObject);
                        break;
                    case SpaceEvent.triggerCutscene:
                        var cutscene = spaceBehavior.cutscene.GetComponent<CutsceneBehavior>();
                        cutscene.ShowMessage(this.gameObject, 0);
                        break;
                    case SpaceEvent.triggerCutsceneWithCollectibles:
                        var cutsceneWithCollectible = spaceBehavior.cutscene.GetComponent<CutsceneBehavior>();
                        cutsceneWithCollectible.ShowMessage(this.gameObject, 1);
                        break;
                    case SpaceEvent.findCollectible:
                        this.FinishVisit(other.gameObject, 1);
                        break;
                    case SpaceEvent.finalEvent:
                        spaceBehavior.HandleFinalEvent(this.gameObject, this.Inventory.Collectibles);
                        break;
                }
                this.PrintInventory();
            }
        }
    }

    private void PrintInventory() {
        Debug.Log("Money: " + this.Inventory.Money + " Nr. of items: " + this.Inventory.Items.Count);
    }

    private void UpdateHUD() {
        if(this.collectiblesText != null && this.gameManager != null) {
            var nrOfCollectiblesToCollect = this.gameManager.GetComponent<GameManagerBehavior>().numberOfCollectiblesNeededToWin;
            this.collectiblesText.GetComponent<Text>().text = "Shells: " + this.Inventory.Collectibles + "/" + nrOfCollectiblesToCollect;    
        }
        if (this.nrOfTurnsText != null && this.gameManager != null) {
			var nrOfTurnsRemaining = this.gameManager.GetComponent<GameManagerBehavior>().numberOfTurnsRemaining;
			this.nrOfTurnsText.GetComponent<Text>().text = "Days remaining: " + nrOfTurnsRemaining;    
        }
    }

    public void Buy(int itemIndex) {
        var item = ItemCreator.CreateItem(itemIndex);
        this.Inventory.BuyItem(item);
        this.PrintInventory();
    }

	internal void FinishVisit(GameObject associatedSpace, int collectibles)
	{
        if(collectibles > 0) {
			if (gameManager != null)
			{
				this.gameManager.ShowCollectibleFoundMessage();
			}
        }
        this.Inventory.FindCollectible(collectibles);
        this.FinishVisit(associatedSpace);
        this.UpdateHUD();
	}

    private void FinishVisit(GameObject space) {
        var spaceBehavior = space.GetComponent<SpaceBehavior>();
        spaceBehavior.visited = true;
        this.gameManager.VisitedSpace(space);
    }
}
