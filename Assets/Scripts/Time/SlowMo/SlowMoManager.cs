using UnityEngine;

public class SlowMoManager : MonoBehaviour {
	public float slowDownFactor = 0.05f;
	public float slowDownLength = 2f;

	void Update() {
		Time.timeScale += (1 / slowDownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
		Time.fixedDeltaTime = Time.timeScale * .02f;

		if (TimeManager.getManager().inSlowMotion()) {
			if (Time.timeScale >= 1f) {
				reset();
			}
		}
	}

	public void doSlowMotion() {
		TimeManager.getManager().setSlowMotion(true);

		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}

	public void reset() {
		TimeManager.getManager().setSlowMotion(false);

		Time.timeScale = 1;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}
}