using System;
using UnityEngine;

public class Interactable : MonoBehaviour {
	//[SerializeField] bool textDisplayed;
	[SerializeField] string displayName;
	[SerializeField] string displayInfo;
	[SerializeField] TextMesh text;
	[SerializeField] GameObject player;

	public Interactable() {}

	public virtual void Start() {
		text.text = "";
		//textDisplayed = false;
	}

	//Getters
	public string getDisplayName() {
		return displayName;
	}

	public string getDisplayInfo() {
		return displayInfo;
	}

	public TextMesh getText() {
		return text;
	}

	public GameObject getPlayer() {
		return player;
	}

	//Functionals
	public virtual void displayText() {
		//textDisplayed = true;
		text.text = displayName + Environment.NewLine + displayInfo;
	}

	public virtual void hideText() {
		//textDisplayed = false;
		text.text = "";
	}

	public virtual void Interact() {}
}