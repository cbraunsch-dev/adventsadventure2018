using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManagerBehavior : MonoBehaviour {
    private const string saveGameFilename = "/SavedGame.dat";
    private int numberOfTurnsRemaining = 0; //TODO: implement this
    private string nameOfCurrentSpace;

    public void TryToMove() {
        SceneManager.LoadScene(SceneNames.Movement);    
    }

    public void VisitedSpace(GameObject space) {
        this.nameOfCurrentSpace = space.name;
        var player = GameObject.FindWithTag(Tags.Player);
        this.SaveGame(player);
    }

    private void SaveGame(GameObject player) {
        var filename = Application.persistentDataPath + saveGameFilename;
        //Debug.Log("Saving file to: " + filename);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

        var gameState = new GameState();
        var mainCamera = GameObject.FindWithTag(Tags.MainCamera);
        gameState.numberOfTurnsRemaining = this.numberOfTurnsRemaining;
        gameState.nameOfCurrentSpace = this.nameOfCurrentSpace;
        gameState.cameraPositionX = mainCamera.transform.position.x;
        gameState.cameraPositionY = mainCamera.transform.position.y;
        gameState.cameraPositionZ = mainCamera.transform.position.z;

		var playerBehavior = player.GetComponent<PlayerBehavior>();
        gameState.playerInventory = playerBehavior.Inventory;
        gameState.playerPositionX = player.transform.position.x;
        gameState.playerPositionY = player.transform.position.y;
        gameState.playerPositionZ = player.transform.position.z;

        bf.Serialize(file, gameState);
        file.Close();
    }

    public GameState LoadGame() {
        var filename = Application.persistentDataPath + saveGameFilename;
        if(File.Exists(filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            var gameState = (GameState)bf.Deserialize(file);
            file.Close();

            this.numberOfTurnsRemaining = gameState.numberOfTurnsRemaining;
            this.nameOfCurrentSpace = gameState.nameOfCurrentSpace;

            return gameState;
        }
        return null;
    }
}

[Serializable]
public class GameState {
    public int numberOfTurnsRemaining;
    public string nameOfCurrentSpace;
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    public float cameraPositionX;
    public float cameraPositionY;
    public float cameraPositionZ;
    public Inventory playerInventory;
}
