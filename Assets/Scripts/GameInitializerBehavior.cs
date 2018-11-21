using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializerBehavior : MonoBehaviour {
    private static GameInitializerBehavior instance = null;

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

	// Use this for initialization
	void Start () {
        switch(ApplicationModel.selectedOptionInStartScreen) {
            case StartScreenOption.newGame:
                Debug.Log("Start new game");
                break;
            case StartScreenOption.loadGame:
                Debug.Log("Load game");
                var gameManager = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
                gameManager.LoadGame();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
