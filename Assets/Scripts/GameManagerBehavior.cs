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
    private GameState gameState;
    private int numberOfMovesPlayerEarned = 0;

	private static GameManagerBehavior instance = null;
	
    void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
		}
		else if (instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
        if(scene.name == Scenes.GameBoard && gameState != null)
        {
            this.ApplyGameState();
            MovePlayerAccordingToEarnedScore();
        }
    }

    private void MovePlayerAccordingToEarnedScore()
    {
        var player = GameObject.FindWithTag(Tags.Player);
        player.GetComponent<PlayerBehavior>().ScheduleMovement(this.numberOfMovesPlayerEarned);
        this.numberOfMovesPlayerEarned = 0;
    }

    public void TryToMove() {
        SceneManager.LoadScene(SceneNames.Movement);    
    }

    public void PlayerEarnedMovementScore(int score) {
        Debug.Log("Player earned score: " + score);
        this.numberOfMovesPlayerEarned = score;
        SceneManager.LoadScene(SceneNames.GameBoard);
    }

    public void VisitedSpace(GameObject space) {
        this.nameOfCurrentSpace = space.name;
        var player = GameObject.FindWithTag(Tags.Player);
        this.SaveGame(player);
    }

    public void LoadGame()
    {
        this.gameState = this.LoadGameStateFromFile();
        ApplyGameState();
    }

    private void SaveGame(GameObject player) {
        var filename = Application.persistentDataPath + saveGameFilename;
        //Debug.Log("Saving file to: " + filename);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

		if (gameState == null)
		{
			gameState = new GameState();
		}
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

    private GameState LoadGameStateFromFile() {
        var filename = Application.persistentDataPath + saveGameFilename;
        if(File.Exists(filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            var loadedGameState = (GameState)bf.Deserialize(file);
            file.Close();

            this.numberOfTurnsRemaining = loadedGameState.numberOfTurnsRemaining;
            this.nameOfCurrentSpace = loadedGameState.nameOfCurrentSpace;

            return loadedGameState;
        }
        return null;
    }

	private void ApplyGameState()
	{
		if (gameState != null)
		{
			this.MoveCameraToSavedLocation(this.gameState);
			var player = GameObject.FindWithTag(Tags.Player);
			var playerBehavior = player.GetComponent<PlayerBehavior>();
			this.MovePlayerToSavedSpace(player, this.gameState);
			playerBehavior.LoadInventory(this.gameState.playerInventory);
		}
	}

	private void MoveCameraToSavedLocation(GameState gameState)
	{
		var mainCamera = GameObject.FindWithTag(Tags.MainCamera);
		mainCamera.transform.position = new Vector3(gameState.cameraPositionX, gameState.cameraPositionY, gameState.cameraPositionZ);
	}

	private void MovePlayerToSavedSpace(GameObject player, GameState gameState)
	{
		var space = GameObject.Find(gameState.nameOfCurrentSpace);
        player.GetComponent<PlayerBehavior>().PlacePlayerAtSpace(space);
		player.transform.position = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);

		//Mark spaces as 'visited'
		this.VisitedSpace(space.GetComponent<SpaceBehavior>());
	}

	private void VisitedSpace(SpaceBehavior spaceBehavior)
	{
		spaceBehavior.visited = true;
		if (spaceBehavior.previousSpace != null)
		{
			this.VisitedSpace(spaceBehavior.previousSpace.GetComponent<SpaceBehavior>());
		}
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
