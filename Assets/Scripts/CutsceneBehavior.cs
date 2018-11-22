using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBehavior : MonoBehaviour {
	private CanvasWithActionsBehavior canvasBehavior;
	private GameObject visitor;

    public GameObject associatedSpace;

	// Use this for initialization
	void Start()
	{
		var textWithActions = transform.Find("Canvas").gameObject;
		this.canvasBehavior = textWithActions.GetComponent<CanvasWithActionsBehavior>();
		textWithActions.SetActive(false);
	}

	public void ShowMessage(GameObject visitor)
	{
        this.visitor = visitor;
		this.canvasBehavior.Show();
	}

	public void Next()
	{
        //TODO: Display next text and hide the canvas once we've reached the end of the text
		this.HideMessage();
	}

	private void HideMessage()
	{
		this.visitor.GetComponent<PlayerBehavior>().FinishVisit(associatedSpace);
		this.canvasBehavior.Hide();
	}
}
