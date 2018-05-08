using UnityEngine;

[RequireComponent(typeof(ObjectState))]
public class Closet : Interactable {
	public bool multiUse;
	public ClosetUI closetUI;

	public Choice[] choices;

	ObjectState state;



	public override void Start() {
		base.Start();

		state = gameObject.GetComponent<ObjectState>();

		if (GameManager.getManager().hasState(state.getId())) {
			state.update(GameManager.getManager().getState(state.getId()));
		}
	}

	public override void Interact() {
		if (closetUI.isOpen) {
			closetUI.closeCloset();
		} else {
			if (!state.hasBeenUsed()) {
				closetUI.openCloset();
			}
		}
	}

	public void handleChoice(ClosetChoice cc) {
		if (cc != null) {
			Choice choice = choices[cc.choiceIndex];

			if (choice.affectStats) {
				if (choice.absoluteStats) {
					GameManager.getManager().affectStats(choice.stats);
				} else {
					GameManager.getManager().adjustStats(choice.stats);
				}
			}

			//Update object state
			state.setUsed(true);
			state.setChangedStats(true);
			GameManager.getManager().updateState(state);

			//Close closet after use.
			closetUI.closeCloset();
		}
	}
}