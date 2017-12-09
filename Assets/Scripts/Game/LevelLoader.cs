using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
	static LevelLoader instance;

	[SerializeField] bool useUI;

	static GameObject loadingScreen;
	static Slider slider;
	static Text progressText;

	void Start() {
		instance = this;

		if (useUI) {
			loadingScreen = GameObject.Find("LoadingScreen");
			slider = GameObject.Find("LoadingScreen.Slider").GetComponent<Slider>();
			progressText = GameObject.Find("LoadingScreen.ProgressText").GetComponent<Text>();

			loadingScreen.SetActive(false);
		}

		GameManager.getManager().onPause += hide;
		GameManager.getManager().onUnpause += hide;
	}

	void OnDestroy() {
		GameManager.getManager().onPause -= hide;
		GameManager.getManager().onUnpause -= hide;
	}



	public static void loadLevel(string scene) {
		instance.StartCoroutine(loadAsynchronously(scene));
	}

	static IEnumerator loadAsynchronously(string scene) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

		if (loadingScreen != null && instance.useUI) {
			loadingScreen.SetActive(true);
		}

		while (!operation.isDone) {

			float progress = Mathf.Clamp01(operation.progress / .9f);

			if (loadingScreen != null && instance.useUI) {
				slider.value = progress;
				progressText.text = progress * 100f + "%";
			}

			yield return null;
		}
	}

	void hide() {
		if (loadingScreen != null) {
			loadingScreen.SetActive(false);
		}
	}
}