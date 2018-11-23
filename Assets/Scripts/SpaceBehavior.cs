using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBehavior : MonoBehaviour {
    public GameObject previousSpace;
    public GameObject nextSpace;
    public SpaceEvent triggeredEvent;
    public bool visited;
    public GameObject store;
    public GameObject cutscene;
    public GameObject victoryCutscene;
    public GameObject defeatCutscene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleFinalEvent(GameObject visitor, int earnedMoney) {
        var gameManagerBehavior = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
        if(earnedMoney >= gameManagerBehavior.amountOfMoneyNeededToWin) {
            var cutsceneBehavior = victoryCutscene.GetComponent<CutsceneBehavior>();
            cutsceneBehavior.ShowMessage(visitor, 0);
        } else {
            var cutsceneBehavior = defeatCutscene.GetComponent<CutsceneBehavior>();
			cutsceneBehavior.ShowMessage(visitor, 0);
        }
    }
}
