using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {
	bool isRewinding;

	public float recordTime = 5f;

	List<PointInTime> pointsInTime;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();

		if (GetComponent<Rigidbody>() != null) {
			rb = GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (isRewinding) {
			rewind();
		} else {
			record();
		}
	}

	void rewind() {
		if (pointsInTime.Count > 0) {
			PointInTime pit = pointsInTime[0];;
			transform.position = pit.position;
			transform.rotation = pit.rotation;
			pointsInTime.RemoveAt(0);
		} else {
			stopRewind();
		}
	}

	void record() {

		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) {
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		} 
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));	
	}

	public void startRewind() {
		isRewinding = true;
		if (rb != null) {
			rb.isKinematic = true;
		}
	}

	public void stopRewind() {
		isRewinding = false;
		if (rb != null) {
			rb.isKinematic = false;
		}
	}
}