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
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
