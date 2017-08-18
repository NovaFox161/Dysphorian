using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour {
	static MenuFunctions instance;
	static Transform thisTransform;

	MenuFunctions() {} //Prevent initialization

	public static MenuFunctions getFunctions() {
		if (instance == null) {
			instance = thisTransform.GetComponent<MenuFunctions>();
		}
		return instance;
	}

	void Awake() {
		thisTransform = gameObject.transform;
	}


	public void determineFucntion(GameMenuItem gmi, Image fadePlane) {
		if (gmi.function.Equals(GameMenuItemFunction.Quit)) {
			quitGame();
		} else if (gmi.function.Equals(GameMenuItemFunction.LoadScene)) {
			StartCoroutine(loadScene(gmi.sceneToLoadName, fadePlane));
		} else if (gmi.function.Equals(GameMenuItemFunction.Unpause)) {
			UnpauseGame();
		} else if (gmi.function.Equals(GameMenuItemFunction.Sub_Menu)) {
			GameMenu.OpenSubMenu(gmi.openMenu);
		} else if (gmi.function.Equals(GameMenuItemFunction.Toggle_Setting)) {
			toggleSetting(gmi);
		}
	}

	public void quitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif

		Application.Quit();
	}

	public IEnumerator loadScene(string _sceneName, Image fadePlane) {
		StartCoroutine(fade(Color.clear, Color.black, 1, fadePlane));
		yield return new WaitForSeconds(1);
		LevelLoader.loadLevel(_sceneName);
	}

	public void UnpauseGame() {
		GameMenu.Unpause();
	}

	public void toggleSetting(GameMenuItem gmi) {
		if (gmi.settingToggle.Equals(ToggleSetting.vSync)) {
			GameManager.getManager().setVSync(!GameManager.getManager().getVSync());
			gmi.refresh();
		} else if (gmi.settingToggle.Equals(ToggleSetting.AntiAliasing)) {
			GameManager.getManager().setAntiAliasing(!GameManager.getManager().getAntiAliasing());
			gmi.refresh();
		} else if (gmi.settingToggle.Equals(ToggleSetting.MotionBlur)) {
			GameManager.getManager().setMotionBlur(!GameManager.getManager().getMotionBlur());
			gmi.refresh();
		}
	}

	public IEnumerator fade(Color from, Color to, float time, Image fadePlane) {
		if (fadePlane != null) {
			float speed = 1 / time;
			float percent = 0;
			while (percent < 1) {
				percent += Time.deltaTime * speed;
				fadePlane.color = Color.Lerp(from, to, percent);
				yield return null;
			}
		}
	}
}