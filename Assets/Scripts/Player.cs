using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(FirstPersonController))]
public class Player	 : MonoBehaviour {
	public bool canInteract;
	public KeyCode interactButton;
	public float interactRange;
	public Camera playerCam;

	InteractableObject lastInteractable;

	void Start() {
		GameManager.getManager().setPlayer(this);
	}

	void Update() {
		if (!GameManager.getManager().isPaused()) {
			//Check if looking at interactables and display text
			InteractableObject iObj = isLookingAtInteractable();
			if (iObj.canUse()) {
				lastInteractable = iObj;
				iObj.getInteractable().displayText();
			} else {
				if (lastInteractable != null) {
					lastInteractable.getInteractable().hideText();
					lastInteractable = null;
				}
			}
			//Check controls
			if (Input.GetKeyDown(interactButton)) {
				if (lastInteractable != null) {
					lastInteractable.getInteractable().Interact();
				}
			}
		}
	}

	public InteractableObject isLookingAtInteractable() {
		if (canInteract) {
			Ray ray = new Ray(transform.position, playerCam.transform.forward);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, interactRange)) {
				if (hit.collider.gameObject.GetComponent<Interactable>() != null) {
					return new InteractableObject(hit.collider.gameObject.GetComponent<Interactable>(), true);
				} else if (hit.collider.gameObject.transform.root != null) {
					if (hit.collider.gameObject.transform.root.GetComponent<Interactable>() != null) {
						return new InteractableObject(hit.collider.gameObject.transform.root.GetComponent<Interactable>(), true);
					}
				}
			}
		}
		return new InteractableObject(null, false);
	}
}

public class InteractableObject {
	readonly Interactable interactable;
	readonly bool canBeUsed;

	public InteractableObject(Interactable _interactable, bool _canUsed) {
		interactable = _interactable;
		canBeUsed = _canUsed;
	}

	public Interactable getInteractable() {
		return interactable;
	}

	public bool canUse() {
		return canBeUsed;
	}
}