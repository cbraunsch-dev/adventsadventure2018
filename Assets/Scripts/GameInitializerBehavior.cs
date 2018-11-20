using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        switch(ApplicationModel.selectedOptionInStartScreen) {
            case StartScreenOption.newGame:
                Debug.Log("Start new game");
                break;
            case StartScreenOption.loadGame:
                Debug.Log("Load game");
                var gameManager = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
                var loadedGame = gameManager.LoadGame();
                this.MoveCameraToSavedLocation(loadedGame);
                var player = GameObject.FindWithTag(Tags.Player);
                var playerBehavior = player.GetComponent<PlayerBehavior>();
                this.MovePlayerToSavedSpace(player, loadedGame);
                playerBehavior.LoadInventory(loadedGame.playerInventory);
                break;
        }
	}

    private void MoveCameraToSavedLocation(GameState gameState) {
        var mainCamera = GameObject.FindWithTag(Tags.MainCamera);
        mainCamera.transform.position = new Vector3(gameState.cameraPositionX, gameState.cameraPositionY, gameState.cameraPositionZ);
    }

    private void MovePlayerToSavedSpace(GameObject player, GameState gameState) {
        var space = GameObject.Find(gameState.nameOfCurrentSpace);
        player.GetComponent<PlayerBehavior>().startingSpace = space;
        player.transform.position = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);

        //Mark spaces as 'visited'
        this.VisitedSpace(space.GetComponent<SpaceBehavior>());
    }

    private void VisitedSpace(SpaceBehavior spaceBehavior) {
		spaceBehavior.visited = true;
		if (spaceBehavior.previousSpace != null)
		{
            this.VisitedSpace(spaceBehavior.previousSpace.GetComponent<SpaceBehavior>());
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
