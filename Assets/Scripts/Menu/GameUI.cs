using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
	static GameUI instance;
	static Transform thisTransform;

	[Header("Options")]
	public bool autoFade;

	[Header("Images")]
	public Image fadePlane;
	public Image fearBg;
	public Image anxietyBg;
	public Image comfortBg;
	public Image depressionBg;

	[Header("Texts")]
	public Text fearText;
	public Text anxietyText;
	public Text comfortText;
	public Text depressionText;

	[Header("Text Display")]
	public string fearDisplay = "Fear: %value%%";
	public string anxietyDisplay = "Anxiety: %value%%";
	public string comfortDisplay = "Comfort: %value%%";
	public string depressionDisplay = "Depression: %value%%";

	[Header("Color Display")]
	public Color goodColor = Color.green;
	public Color medColor = Color.yellow;
	public Color badColor = Color.red;



	//Instance handling
	GameUI() {
	} //Prevent initialization.

	public static GameUI getUI() {
		if (instance == null) {
			instance = thisTransform.GetComponent<GameUI>();
		}
		return instance;
	}

	void Awake() {
		thisTransform = gameObject.transform;
	}

	// Use this for initialization
	void Start() {
		GameManager.getManager().onPause += onPause;
		GameManager.getManager().onUnpause += onUnpause;
		GameManager.getManager().onFearChange += updateFearText;
		GameManager.getManager().onAnxietyChange += updateAnxietyText;
		GameManager.getManager().onComfortChange += updateComfortText;
		GameManager.getManager().onDepressionChange += updateDepressionText;

		updateFearText(GameManager.getManager().getFearPercent());
		updateAnxietyText(GameManager.getManager().getAnxietyPercent());
		updateComfortText(GameManager.getManager().getComfortPercent());
		updateDepressionText(GameManager.getManager().getDepressionPercent());

		if (autoFade) {
			StartCoroutine(fade(Color.black, Color.clear, 1f));
		}
	}

	void OnDestroy() {
		GameManager.getManager().onPause -= onPause;
		GameManager.getManager().onUnpause -= onUnpause;
		GameManager.getManager().onFearChange -= updateFearText;
		GameManager.getManager().onAnxietyChange -= updateAnxietyText;
		GameManager.getManager().onComfortChange -= updateComfortText;
		GameManager.getManager().onDepressionChange -= updateDepressionText;
	}

	void onPause() {
		if (autoFade) {
			StartCoroutine(fade(Color.clear, Color.black, 1f));
		}

		int childCount = transform.childCount;
		for (int i = 1; i < childCount; i++) {
			GameObject go = transform.GetChild(i).gameObject;
			if (go != null) {
				go.SetActive(false);
			}
		}
	}

	void onUnpause() {
		if (autoFade) {
			StartCoroutine(fade(Color.black, Color.clear, 1f));
		}

		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			GameObject go = transform.GetChild(i).gameObject;
			if (go != null) {
				go.SetActive(true);
			}
		}
	}

	public IEnumerator fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from, to, percent);
			yield return null;
		}
	}

	public IEnumerator fade(float from, float to, float time, AudioSource audio) {
		float speed = 1 / time;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			audio.volume = Mathf.Lerp(from, to, percent);
			yield return null;
		}
	}

	#region Public Functionals
	public void updateFearText(int prev) {
		int f = GameManager.getManager().getFearPercent();

		StartCoroutine(fadeNumberDisplay(prev, f, fearDisplay, "%value%", fearText, 1f));

		if (f < 50) {
			StartCoroutine(fadeColorChange(fearBg.color, goodColor, fearBg, 1f));
		} else if (f >= 50 && f <= 75) {
			StartCoroutine(fadeColorChange(fearBg.color, medColor, fearBg, 1f));
		} else if (f >= 76) {
			StartCoroutine(fadeColorChange(fearBg.color, badColor, fearBg, 1f));
		}
	}

	public void updateAnxietyText(int prev) {
		int f = GameManager.getManager().getAnxietyPercent();

		StartCoroutine(fadeNumberDisplay(prev, f, anxietyDisplay, "%value%", anxietyText, 1f));

		if (f < 50) {
			StartCoroutine(fadeColorChange(anxietyBg.color, goodColor, anxietyBg, 1f));
		} else if (f >= 50 && f <= 75) {
			StartCoroutine(fadeColorChange(anxietyBg.color, medColor, anxietyBg, 1f));
		} else if (f >= 76) {
			StartCoroutine(fadeColorChange(anxietyBg.color, badColor, anxietyBg, 1f));
		}
	}

	public void updateComfortText(int prev) {
		int f = GameManager.getManager().getComfortPercent();

		StartCoroutine(fadeNumberDisplay(prev, f, comfortDisplay, "%value%", comfortText, 1f));

		if (f <= 25) {
			StartCoroutine(fadeColorChange(comfortBg.color, badColor, comfortBg, 1f));
		} else if (f >= 26 && f < 51) {
			StartCoroutine(fadeColorChange(comfortBg.color, medColor, comfortBg, 1f));
		} else if (f >= 51) {
			StartCoroutine(fadeColorChange(comfortBg.color, goodColor, comfortBg, 1f));
		}
	}

	public void updateDepressionText(int prev) {
		int f = GameManager.getManager().getDepressionPercent();

		StartCoroutine(fadeNumberDisplay(prev, f, depressionDisplay, "%value%", depressionText, 1f));

		if (f < 50) {
			StartCoroutine(fadeColorChange(depressionBg.color, goodColor, depressionBg, 1f));
		} else if (f >= 50 && f <= 75) {
			StartCoroutine(fadeColorChange(depressionBg.color, medColor, depressionBg, 1f));
		} else if (f >= 76) {
			StartCoroutine(fadeColorChange(depressionBg.color, badColor, depressionBg, 1f));
		}
	}

	IEnumerator fadeColorChange(Color from, Color to, Image image, float time) {
		float speed = 1 / time;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			image.color = Color.Lerp(from, to, percent);
			yield return null;
		}
	}

	IEnumerator fadeNumberDisplay(int from, int to, string raw, string replace, Text text, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;

			int number = Mathf.RoundToInt(Mathf.Lerp(from, to, percent));

			string good = raw.Replace(replace, number.ToString());

			text.text = good;

			yield return null;
		}
	}
	#endregion
}