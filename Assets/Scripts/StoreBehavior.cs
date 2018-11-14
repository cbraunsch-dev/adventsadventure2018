using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour {
    private CanvasWithActionsBehavior canvasBehavior;

	// Use this for initialization
	void Start () {
        var textWithActions = transform.Find("Canvas").gameObject;
        this.canvasBehavior = textWithActions.GetComponent<CanvasWithActionsBehavior>();
        textWithActions.SetActive(false);
	}

    public void ShowMessage() {
        this.canvasBehavior.Show();
    }
}
