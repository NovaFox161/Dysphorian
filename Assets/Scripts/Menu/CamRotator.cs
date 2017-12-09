using UnityEngine;

public class CamRotator : MonoBehaviour {
	public float speed = 1;

	void Update () {
		Vector3 currentRot = gameObject.transform.rotation.eulerAngles;
		Vector3 newRotation = new Vector3(0, currentRot.y + (speed * Time.deltaTime), 0);

		gameObject.transform.rotation = Quaternion.Euler(newRotation);
	}
}