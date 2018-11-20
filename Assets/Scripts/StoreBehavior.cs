using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBehavior : MonoBehaviour {
    private CanvasWithActionsBehavior canvasBehavior;
    private GameObject visitor;

    public GameObject associatedSpace;

	// Use this for initialization
	void Start () {
        var textWithActions = transform.Find("Canvas").gameObject;
        this.canvasBehavior = textWithActions.GetComponent<CanvasWithActionsBehavior>();
        textWithActions.SetActive(false);
	}

    public void ShowMessage(GameObject visitor) {
        this.visitor = visitor;
        this.canvasBehavior.Show();
    }

	public void Buy(int itemIndex)
	{
        this.visitor.GetComponent<PlayerBehavior>().Buy(itemIndex);
        this.HideMessage();
	}

    public void HideMessage() {
        this.visitor.GetComponent<PlayerBehavior>().FinishVisit(associatedSpace);
        this.canvasBehavior.Hide();
    }
}
