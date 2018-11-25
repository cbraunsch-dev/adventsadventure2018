using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleFoundCanvasBehavior : MonoBehaviour {
    private GameObject gameManager;


	// Use this for initialization
	void Start () {
        this.gameManager = GameObject.FindWithTag(Tags.GameManager);
        var okButton = GameObject.Find("CollectibleFoundOkButton");
        okButton.GetComponent<Button>().onClick.AddListener(() => { gameManager.GetComponent<GameManagerBehavior>().HideCollectibleFoundMessage(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
