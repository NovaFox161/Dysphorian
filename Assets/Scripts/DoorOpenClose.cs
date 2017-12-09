using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DoorOpenClose : Interactable {
	public bool open;
	public string openAnim;
	public string closeAnim;
	public AudioClip openSound;
	public AudioClip closeSound;

	Animator animator;
	AudioSource audioSource;

	void Awake() {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	public override void Interact() {
		if (open) {
			open = false;
			animator.Play(closeAnim);
			if (closeSound != null) {
				audioSource.PlayOneShot(closeSound);
			}
		} else {
			open = true;
			animator.Play(openAnim);
			if (openSound != null) {
				audioSource.PlayOneShot(openSound);
			}
		}
	}
}