using UnityEngine;

public class TimeManager : MonoBehaviour {
	static TimeManager instance;

	SlowMoManager slowMoManager;

	bool slowMo;

	TimeManager() {
	} //Prevent initialization

	public static TimeManager getManager() {
		return instance;
	}

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;

			DontDestroyOnLoad(gameObject);

			//Assign our defaults.
			slowMoManager = gameObject.GetComponent<SlowMoManager>();
		}
	}

	#region Getters
	public SlowMoManager getSlowMoManager() {
		return slowMoManager;
	}
	#endregion

	#region Setters
	public void setSlowMotion(bool _slowMo) {
		slowMo = _slowMo;
	}
	#endregion

	#region Bools/Checkers
	public bool inSlowMotion() {
		return slowMo;
	}
	#endregion
}