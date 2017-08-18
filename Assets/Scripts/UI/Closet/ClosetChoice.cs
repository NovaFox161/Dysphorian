using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ClosetChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public ClosetUI closetUI;

	public int choiceIndex;

	Text text;

	void Start() {
		text = GetComponent<Text>();

		text.color = closetUI.normalColor;
	}

	void OnEnable() {
		text = GetComponent<Text>();

		text.color = closetUI.normalColor;
	}


	public void OnPointerEnter(PointerEventData eventData) {
		text.color = closetUI.hoverColor;
		closetUI.mouseOver = this;
	}

	public void OnPointerExit(PointerEventData eventData) {
		text.color = closetUI.normalColor;
		closetUI.mouseOver = null;
	}
}