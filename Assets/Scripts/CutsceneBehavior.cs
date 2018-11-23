using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneBehavior : MonoBehaviour {
	private CanvasWithActionsBehavior canvasBehavior;
	private GameObject visitor;
    private int textIndex = 0;
    private GameObject textComponent;
    private GameObject buttonTextComponent;
    private int collectibles = 0;

    public GameObject associatedSpace;
    public List<string> texts = new List<string>();

	// Use this for initialization
	void Start()
	{
		var textWithActions = transform.Find("Canvas").gameObject;
        var imageBackground = textWithActions.transform.Find("Image");
        this.textComponent = imageBackground.Find("Text").gameObject;
        var button = imageBackground.Find("Button");
        this.buttonTextComponent = button.Find("Text").gameObject;
		this.canvasBehavior = textWithActions.GetComponent<CanvasWithActionsBehavior>();
		textWithActions.SetActive(false);
	}

    public void ShowMessage(GameObject visitor, int collectibles) {
        this.collectibles = collectibles;
		this.textIndex = 0;
		this.visitor = visitor;
		this.canvasBehavior.Show();
		this.ShowNextText();
    }

	public void Next()
	{
        //Display next text and hide the canvas once we've reached the end of the text
        if(this.textIndex < texts.Count) {
            this.ShowNextText();    
        } else {
            this.HideMessage();    
        }
	}

    private void ShowNextText() {
        var text = this.texts[this.textIndex];
        textComponent.GetComponent<Text>().text = text;
        this.textIndex++;

        if(this.textIndex >= this.texts.Count) {
            buttonTextComponent.GetComponent<Text>().text = "Ok";
        }
    }

	private void HideMessage()
	{
        this.visitor.GetComponent<PlayerBehavior>().FinishVisit(associatedSpace, this.collectibles);
		this.canvasBehavior.Hide();
	}
}
