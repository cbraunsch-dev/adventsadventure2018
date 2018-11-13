using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour {
    public string messageName;
    public string messageText;
    private CanvasWithActionsBehavior canvasBehavior;

	// Use this for initialization
	void Start () {
        var textWithActions = GameObject.FindWithTag(Tags.CanvasWithActions);
        this.canvasBehavior = textWithActions.GetComponent<CanvasWithActionsBehavior>();
        textWithActions.SetActive(false);
	}

    public void ShowMessage() {
        this.canvasBehavior.Show(messageName, messageText);
    }
}
