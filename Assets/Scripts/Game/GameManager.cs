using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	static GameManager instance;

	[Header("Game Settings")]
	[SerializeField] bool vSync;
	[SerializeField] bool antialiazing = true;
	[SerializeField] bool motionBlur = true;

	[Header("Game Variables")]
	[SerializeField] bool paused;

	[Header("Player Stats")]
	[SerializeField] int fearPercent = 50;
	[SerializeField] int anxietyPercent = 50;
	[SerializeField] int comfortPercent = 50;
	[SerializeField] int depressionPercent = 50;

	//Other stuff
	Player player;

	List<ObjectState> objectStates = new List<ObjectState>();

	//Events
	public event Action onPause;
	public event Action onUnpause;

	public event Action<int> onFearChange;
	public event Action<int> onAnxietyChange;
	public event Action<int> onComfortChange;
	public event Action<int> onDepressionChange;

	GameManager() {} //Prevent initialization

	public static GameManager getManager() {
		return instance;
	}

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);

			instance = this;
			paused = false;
			SettingsManager.setVSync(vSync);
			SettingsManager.setAntiAliasing(antialiazing);
			SettingsManager.setMotionBlur(motionBlur);
		}
	}

	public bool hasState(string id) {
		foreach (ObjectState os in objectStates) {
			if (os.getId().Equals(id)) {
				return true;
			}
		}
		return false;
	}

	#region Public Getters
	public bool getVSync() {
		return vSync;
	}

	public bool getAntiAliasing() {
		return antialiazing;
	}

	public bool getMotionBlur() {
		return motionBlur;
	}

	public bool isPaused() {
		return paused;
	}

	public int getFearPercent() {
		return fearPercent;
	}

	public int getAnxietyPercent() {
		return anxietyPercent;
	}

	public int getComfortPercent() {
		return comfortPercent;
	}

	public int getDepressionPercent() {
		return depressionPercent;
	}

	public ObjectState getState(string id) {
		foreach (ObjectState os in objectStates) {
			if (os.getId().Equals(id)) {
				return os;
			}
		}

		return null;
	}

	public Player getPlayer() {
		return player;
	}
	#endregion

	#region Public Setters
	public void setVSync(bool _vSync) {
		vSync = _vSync;
		SettingsManager.setVSync(vSync);
	}

	public void setAntiAliasing(bool _aa) {
		antialiazing = _aa;
		SettingsManager.setAntiAliasing(antialiazing);
	}

	public void setMotionBlur(bool _mb) {
		motionBlur = _mb;
		SettingsManager.setMotionBlur(motionBlur);
	}


	public void setPaused(bool _paused) {
		paused = _paused;
	}

	public void setFearPercent(int _fear) {
		int prev = fearPercent;
		fearPercent = _fear;
		if (onFearChange != null) {
			onFearChange(prev);
		}
	}

	public void setAnxietyPercent(int _anxiety) {
		int prev = anxietyPercent;
		anxietyPercent = _anxiety;
		if (onAnxietyChange != null) {
			onAnxietyChange(prev);
		}
	}

	public void setComfortPercent(int _comfort) {
		int prev = comfortPercent;
		comfortPercent = _comfort;
		if (onComfortChange != null) {
			onComfortChange(prev);
		}
	}

	public void setDepressionPercent(int _depression) {
		int prev = depressionPercent;
		depressionPercent = _depression;
		if (onDepressionChange != null) {
			onDepressionChange(prev);
		}
	}

	public void updateState(ObjectState state) {
		removeState(state.getId());
		objectStates.Add(state);
	}

	public void setPlayer(Player _player) {
		player = _player;
	}
	#endregion

	#region Public Functionals
	public void removeState(string id) {
		if (hasState(id)) {
			objectStates.Remove(getState(id));
		}
	}

	public void showMouse() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void hideMouse() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void pause() {
		paused = true;
		if (onPause != null) {
			onPause();
		}
	}

	public void unpause() {
		paused = false;
		if (onUnpause != null) {
			onUnpause();
		}
	}

	public void affectStats(PlayerStats stats) {
		if (stats.fear != -1) {
			setFearPercent(stats.fear);
		}
		if (stats.anxiety != -1) {
			setAnxietyPercent(stats.anxiety);
		}
		if (stats.comfort != -1) {
			setComfortPercent(stats.comfort);
		}
		if (stats.depression != -1) {
			setDepressionPercent(stats.depression);
		}
	}

	public void adjustStats(PlayerStats stats) {
		setFearPercent(Mathf.Clamp(getFearPercent() + stats.fear, 0, 100));
		setAnxietyPercent(Mathf.Clamp(getAnxietyPercent() + stats.anxiety, 0, 100));
		setComfortPercent(Mathf.Clamp(getComfortPercent() + stats.comfort, 0, 100));
		setDepressionPercent(Mathf.Clamp(getDepressionPercent() + stats.depression, 0, 100));
	}
	#endregion
}

[Serializable]
public struct PlayerStats {
	public int fear;
	public int anxiety;
	public int comfort;
	public int depression;
}