using UnityEngine;

public class ClosetUI : MonoBehaviour {
	public Closet closet;

	public Color normalColor;
	public Color hoverColor;
	public Color clickColor;

	public ClosetChoice mouseOver;

	public bool isOpen;

	public void Start() {
		closeCloset();

		GameManager.getManager().onPause += CloseClosetOnPause;
		GameManager.getManager().onUnpause += OpenClosetOnUnPause;
	}

	void OnDestroy() {
		GameManager.getManager().onPause -= CloseClosetOnPause;
		GameManager.getManager().onUnpause -= OpenClosetOnUnPause;
	}

	public void Update() {
		if (isOpen) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				if (mouseOver != null) {
					//Send to closet object to handle.
					closet.handleChoice(mouseOver);
				}
			}
		}
	}

	public void openCloset() {
		isOpen = true;

		for (int i = 0; i<transform.childCount; i++) {
			GameObject g = transform.GetChild(i).gameObject;

			g.SetActive(true); 
		}

		FirstPersonDisable.disable();

		GameManager.getManager().showMouse();
	}

	public void closeCloset() {
		isOpen = false;
		mouseOver = null;

		for (int i = 0; i<transform.childCount; i++) {
			GameObject g = transform.GetChild(i).gameObject;

			g.SetActive(false); 
		}
		FirstPersonDisable.enable();

		GameManager.getManager().hideMouse();
	}

	void CloseClosetOnPause() {
		if (isOpen) {
			for (int i = 0; i < transform.childCount; i++) {
				GameObject g = transform.GetChild(i).gameObject;

				g.SetActive(false);
			}
			mouseOver = null;
		}
	}

	void OpenClosetOnUnPause() {
		if (isOpen) {
			for (int i = 0; i<transform.childCount; i++) {
				GameObject g = transform.GetChild(i).gameObject;

				g.SetActive(true);
			}
			FirstPersonDisable.disable();

			GameManager.getManager().showMouse();
		}
	}
}