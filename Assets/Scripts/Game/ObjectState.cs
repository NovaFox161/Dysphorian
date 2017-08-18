using UnityEngine;

public class ObjectState : MonoBehaviour {
	[SerializeField]
	string id;

	bool on;
	bool changedStats;
	bool used;

	//Getters
	public string getId() {
		return id;
	}

	public bool isOn() {
		return on;
	}

	public bool hasChangedStats() {
		return changedStats;
	}

	public bool hasBeenUsed() {
		return used;
	}

	//Setters
	public void setId(string _id) {
		id = _id;
	}

	public void setOn(bool _on) {
		on = _on;
	}

	public void setChangedStats(bool _changed) {
		changedStats = _changed;
	}

	public void setUsed(bool _used) {
		used = _used;
	}

	//Misc
	public void update(ObjectState s) {
		on = s.isOn();
		changedStats = s.hasChangedStats();
		used = s.hasBeenUsed();
	}
}