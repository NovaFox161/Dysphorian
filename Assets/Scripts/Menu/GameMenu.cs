using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
	public GameMenuType type;
	public ItemSubMenu startOpen = ItemSubMenu.Main;
	public bool openOnStart;
	public bool showMouseWhenOpen;
	public bool hideMouseOnClose;
	public Color normalColor = Color.black;
	public Color hoverColor = Color.green;
	public Color unusableColor = Color.gray;
	public KeyCode pauseButton;
	public GameMenuItem mouseOver;
	public Image fadePlane;

	static bool isOpen;
	static List<GameMenuItem> menuItems;

	#region Unity methods
	public void Awake() {
		menuItems = new List<GameMenuItem>();
		foreach (GameMenuItem gmi in FindObjectsOfType<GameMenuItem>()) {
			menuItems.Add(gmi);
		}
	}
		
	public void Start() {
		if (type.Equals(GameMenuType.Main)) {
			GameManager.getManager().setPaused(false);
			StartCoroutine(MenuFunctions.getFunctions().fade(Color.white, Color.clear, 5f, fadePlane));
		}
		if (openOnStart) {
			isOpen = true;
			if (showMouseWhenOpen) {
				GameManager.getManager().showMouse();
			} else {
				GameManager.getManager().hideMouse();
			}

			//Hide all subMenus
			foreach (GameMenuItem gmi in menuItems) {
				if (!gmi.subMenu.Equals(startOpen)) {
					gmi.gameObject.SetActive(false);
				}
			}
		} else {
			//Hide everything
			foreach (GameMenuItem gmi in menuItems) {
				gmi.gameObject.SetActive(false);
			}
			if (hideMouseOnClose) {
				GameManager.getManager().hideMouse();
			}
		}
	}

	public void Update() {
		if (isOpen) {
			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				if (mouseOver != null) {
					MenuFunctions.getFunctions().determineFucntion(mouseOver, fadePlane);
				}
			}
		}
		if (type.Equals(GameMenuType.Pause)) {
			if (Input.GetKeyDown(pauseButton)) {
				if (GameManager.getManager().isPaused()) {
					Unpause();
					if (hideMouseOnClose) {
						GameManager.getManager().hideMouse();
					}
				} else {
					Pause();
					if (showMouseWhenOpen) {
						GameManager.getManager().showMouse();
					}
				}
			}
		}
	}
	#endregion

	public static void OpenSubMenu(ItemSubMenu sm) {
		foreach (GameMenuItem gmi in menuItems) {
			if (gmi.subMenu.Equals(sm)) {
				gmi.gameObject.SetActive(true);
			} else {
				gmi.gameObject.SetActive(false);
			}
		}
	}

	public static void Pause() {
		foreach (GameMenuItem gmi in menuItems) {
			gmi.gameObject.SetActive(true);
		}
		isOpen = true;
		GameManager.getManager().pause();
	}

	public static void Unpause() {
		foreach (GameMenuItem gmi in menuItems) {
			gmi.gameObject.SetActive(false);
		}
		isOpen = false;
		GameManager.getManager().unpause();
	}


}

public enum GameMenuType {
	Main, Pause
}