﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StairTransition : Interactable {
	public bool fade;
	public bool playSound;
	public bool loadScene;
	public string sceneName;
	public AudioClip sound;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	public override void Interact() {
		if (loadScene) {
			if (fade) {
				if (playSound && sound != null)
					audioSource.PlayOneShot(sound);

				StartCoroutine(loadSceneAfterFade(Color.clear, Color.black, 3));
			} else {
				LevelLoader.loadLevel(sceneName);
				if (playSound && sound != null) {
					audioSource.PlayOneShot(sound);
				}
			}
		}
	}

	IEnumerator loadSceneAfterFade(Color from, Color to, float time) {
		StartCoroutine(GameUI.getUI().fade(from, to, time));
		StartCoroutine(GameUI.getUI().fade(1, 0, time, audioSource));
		yield return new WaitForSeconds(time);
		LevelLoader.loadLevel(sceneName);
	}
}