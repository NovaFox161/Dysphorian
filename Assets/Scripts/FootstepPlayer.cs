using UnityEngine;
using System;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(FirstPersonController))]
public class FootstepPlayer : MonoBehaviour {
	public List<Sounds> surfaceSounds;

	AudioSource audioSource;
	FirstPersonController fps;

	// Use this for initialization
	void Start() {
		audioSource = GetComponent<AudioSource>();
		fps = GetComponent<FirstPersonController>();
		fps.onPlayStepSound += playFootStepSound;
	}

	void playFootStepSound() {
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, 10)) {
			WalkableSurface ws = hit.collider.GetComponent<WalkableSurface>();
			if (ws != null) {
				Sounds sounds = getSounds(ws.material);
				if (sounds != null && sounds.steps.Length > 1) {
					//Play sounds!
					int n = Random.Range(1, sounds.steps.Length);
					audioSource.clip = sounds.steps[n];
					audioSource.PlayOneShot(audioSource.clip);
					sounds.steps[n] = sounds.steps[0];
					sounds.steps[0] = audioSource.clip;
				}
			}
		}
	}

	Sounds getSounds(SurfaceMaterial mat) {
		foreach (Sounds s in surfaceSounds) {
			if (s.material.Equals(mat)) {
				return s;
			}
		}
		return null;
	}
}

public enum SurfaceMaterial {
	CarpetFloor,
	WoodFloor,
	Concrete_Floor,
	Tile_Floor,
	Grass_Ground
}

[Serializable]
public class Sounds {
	public SurfaceMaterial material;
	public AudioClip[] steps;
}