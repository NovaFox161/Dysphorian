using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class SettingsManager : MonoBehaviour {

	public static void setVSync(bool vSync) {
		QualitySettings.vSyncCount = vSync ? 0 : 1;
	}

	public static void setAntiAliasing(bool aa) {
		foreach (Camera c in Camera.allCameras) {
			if (c.GetComponent<Antialiasing>() != null) {
				c.GetComponent<Antialiasing>().enabled = aa;
			}
		}
	}

	public static void setMotionBlur(bool mb) {
		foreach (Camera c in Camera.allCameras) {
			if (c.GetComponent<CameraMotionBlur>() != null) {
				c.GetComponent<CameraMotionBlur>().enabled = mb;
			}
			if (c.GetComponent<MotionBlur>() != null) {
				c.GetComponent<MotionBlur>().enabled = mb;
			}
		}
	}
}