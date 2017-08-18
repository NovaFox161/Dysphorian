using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonDisable : MonoBehaviour {

	public void Start() {
		GameManager.getManager().onPause += disable;
		GameManager.getManager().onUnpause += enable;
	}

	public void OnDestroy() {
		GameManager.getManager().onPause -= disable;
		GameManager.getManager().onUnpause -= enable;
	}

	public static void disable() {
		if (GameManager.getManager().getPlayer() != null) {
			GameManager.getManager().getPlayer().GetComponent<FirstPersonController>().enabled = false;
		}
	}

	public static void enable() {
		if (GameManager.getManager().getPlayer() != null) {
			GameManager.getManager().getPlayer().GetComponent<FirstPersonController>().enabled = true;
		}
	}
}