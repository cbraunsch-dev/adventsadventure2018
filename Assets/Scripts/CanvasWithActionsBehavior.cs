using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWithActionsBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(string name, string text) {
        var nameLabel = GameObject.FindWithTag(Tags.CanvasWithActionsName);
        nameLabel.GetComponent<Text>().text = name;
        var messageLabel = GameObject.FindWithTag(Tags.CanvasWithActionsText);
        messageLabel.GetComponent<Text>().text = text;
    }
}
