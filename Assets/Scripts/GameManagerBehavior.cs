using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManagerBehavior : MonoBehaviour {
    private const string saveGameFilename = "/SavedGame.dat";
    private string nameOfCurrentSpace;
    private GameState gameState;
    private int numberOfMovesPlayerEarned = 0;
    private GameObject outOfTurnsCanvas;
    private GameObject foundCollectibleCanvas;
    private GameObject letsGoButtonValue;
    private GameObject letsGoButton { get {
            if (this.letsGoButtonValue == null) {
                this.letsGoButtonValue = GameObject.Find("LetsGoButton");
            }
            return this.letsGoButtonValue;
        } 
    }

	private static GameManagerBehavior instance = null;

    public int numberOfTurnsRemaining { get; private set; }
    public int numberOfCollectiblesNeededToWin = 2;
    public bool tutorialMode = false;
	
    void Awake()
	{
		if (instance == null)
		{
            this.numberOfTurnsRemaining = 10;
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

    void Start() {
        this.outOfTurnsCanvas = this.FindOutOfTurnsCanvas();
        this.outOfTurnsCanvas.SetActive(false);
        this.foundCollectibleCanvas = this.FindCollectibleFoundCanvas();
        this.foundCollectibleCanvas.SetActive(false);
        this.letsGoButton.SetActive(true);
    }

    private GameObject FindOutOfTurnsCanvas() {
        return GameObject.Find("OutOfTurnsCanvas");
    }

    private GameObject FindCollectibleFoundCanvas() {
        return GameObject.Find("CollectibleFoundCanvas");
    }

    public void ShowCollectibleFoundMessage() {
        this.foundCollectibleCanvas.SetActive(true);
    }

    public void HideCollectibleFoundMessage() {
        this.foundCollectibleCanvas.SetActive(false);
    }

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
        if((scene.name == Scenes.GameBoard || scene.name == Scenes.TutorialBoard) && 
           gameState != null)
        {
            this.outOfTurnsCanvas = this.FindOutOfTurnsCanvas();
            this.outOfTurnsCanvas.SetActive(false);
			this.foundCollectibleCanvas = this.FindCollectibleFoundCanvas();
			this.foundCollectibleCanvas.SetActive(false);
            this.letsGoButton.SetActive(true);
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
        this.letsGoButton.SetActive(false);
        SceneManager.LoadScene(SceneNames.Movement);    
    }

    public void PlayerEarnedMovementScore(int score) {
        Debug.Log("Player earned score: " + score);
        this.numberOfMovesPlayerEarned = score;
        if(this.tutorialMode) {
            SceneManager.LoadScene(SceneNames.TutorialBoard);
		} else {
            SceneManager.LoadScene(SceneNames.GameBoard);    
        }
    }

    public void VisitedSpace(GameObject space) {
        this.nameOfCurrentSpace = space.name;
        var player = GameObject.FindWithTag(Tags.Player);
        this.gameState = this.RecordGameState(player);
        if(!this.tutorialMode) {
			this.numberOfTurnsRemaining--;
			if (this.numberOfTurnsRemaining == 0 && space.GetComponent<SpaceBehavior>().triggeredEvent != SpaceEvent.finalEvent)
			{
				//Game over
				this.outOfTurnsCanvas.SetActive(true);
			}
			else
			{
                this.SerializeGameState(gameState);
			}    
        }
    }

    public void LoadGame()
    {
        this.gameState = this.LoadGameStateFromFile();
        ApplyGameState();
    }

    private GameState RecordGameState(GameObject player)
	{
        var state = new GameState();
		var mainCamera = GameObject.FindWithTag(Tags.MainCamera);
		state.numberOfTurnsRemaining = this.numberOfTurnsRemaining;
		state.nameOfCurrentSpace = this.nameOfCurrentSpace;
		state.cameraPositionX = mainCamera.transform.position.x;
		state.cameraPositionY = mainCamera.transform.position.y;
		state.cameraPositionZ = mainCamera.transform.position.z;

		var playerBehavior = player.GetComponent<PlayerBehavior>();
		state.playerInventory = playerBehavior.Inventory;
		state.playerPositionX = player.transform.position.x;
		state.playerPositionY = player.transform.position.y;
		state.playerPositionZ = player.transform.position.z;

        return state;
	}

    private void SerializeGameState(GameState state)
    {
        var filename = Application.persistentDataPath + saveGameFilename;
        //Debug.Log("Saving file to: " + filename);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

        bf.Serialize(file, state);
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

	private void MovePlayerToSavedSpace(GameObject player, GameState state)
	{
		var space = GameObject.Find(state.nameOfCurrentSpace);
        player.GetComponent<PlayerBehavior>().PlacePlayerAtSpace(space);
		player.transform.position = new Vector3(state.playerPositionX, state.playerPositionY, state.playerPositionZ);

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
