using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string display;
	public ItemSubMenu subMenu = ItemSubMenu.Main;
	public bool allowInteraction;
	public GameMenu gameMenu;
	public GameMenuItemFunction function;
	public string sceneToLoadName;
	public ItemSubMenu openMenu = ItemSubMenu.None;
	public ToggleSetting settingToggle = ToggleSetting.None;

	Text text;

	void Start() {
		if (GetComponent<Text>() != null) {
			text = GetComponent<Text>();
			if (!allowInteraction && !function.Equals(GameMenuItemFunction.None)) {
				GetComponent<Text>().color = gameMenu.unusableColor;
			} else {
				GetComponent<Text>().color = gameMenu.normalColor;
			}
		}
		refresh();
	}

	void OnEnable() {
		if (GetComponent<Text>() != null) {
			if (!allowInteraction && !function.Equals(GameMenuItemFunction.None)) {
				GetComponent<Text>().color = gameMenu.unusableColor;
			} else {
				GetComponent<Text>().color = gameMenu.normalColor;
			}
		}
	}

	#region IPointerEnterHandler implementation
	public void OnPointerEnter(PointerEventData eventData) {
		if (allowInteraction) {
			gameMenu.mouseOver = GetComponent<GameMenuItem>();
		}
		if (GetComponent<Text>() != null) {
			if (allowInteraction && !function.Equals(GameMenuItemFunction.None)) {
				GetComponent<Text>().color = gameMenu.hoverColor;
			}
		}
	}
	#endregion

	public void OnPointerExit(PointerEventData eventData) {
		if (allowInteraction) {
			gameMenu.mouseOver = null;
		}
		if (GetComponent<Text>() != null) {
			if (allowInteraction && !function.Equals(GameMenuItemFunction.None)) {
				GetComponent<Text>().color = gameMenu.normalColor;
			}
		}
	}

	public void refresh() {
		if (text != null) {
			if (function.Equals(GameMenuItemFunction.Toggle_Setting)) {
				if (settingToggle.Equals(ToggleSetting.vSync)) {
					text.text = display.Replace("%value%", GameManager.getManager().getVSync().ToString());
				} else if (settingToggle.Equals(ToggleSetting.AntiAliasing)) {
					text.text = display.Replace("%value%", GameManager.getManager().getAntiAliasing().ToString());
				} else if (settingToggle.Equals(ToggleSetting.MotionBlur)) {
					text.text = display.Replace("%value%", GameManager.getManager().getMotionBlur().ToString());
				}
			}
		}
	}
}

public enum GameMenuItemFunction {
	None, Quit, LoadScene, Unpause, Sub_Menu, Toggle_Setting
}

public enum ItemSubMenu {
	None, Main, Settings, Settins_Audio, Settings_Video
}

public enum ToggleSetting {
	None, vSync, AntiAliasing, MotionBlur
}