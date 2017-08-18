using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	static AudioManager instance;

	public Sound[] sounds;

	AudioManager() {
	} //Prevent initialization

	public static AudioManager getManager() {
		return instance;
	}

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;

			DontDestroyOnLoad(gameObject);

			foreach (Sound s in sounds) {
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
			}
		}
	}

	public void play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null) {
			s.source.Play();
		} else {
			Debug.LogWarning("Sound: \"" + name + "\" not found!");
		}
	}

	public void play(string name, float fadeTime) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null) {
			s.source.volume = 0f;
			s.source.Play();
			fadeIn(s, fadeTime);
		} else {
			Debug.LogWarning("Sound: \"" + name + "\" not found!");
		}
	}

	public void stop(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null) {
			s.source.Stop();
		} else {
			Debug.LogWarning("Sound : \"" + name + "\" not found!");
		}
	}

	public void stop(string name, float fadeTime) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null) {
			fadeOut(s, fadeTime);
		} else {
			Debug.LogWarning("Sound: \"" + name + "\" not found!");
		}
	}

	IEnumerator fadeIn(Sound sound, float fadeTime) {
		sound.source.volume = 0f;
		float speed = 1 / fadeTime;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			sound.source.volume = Mathf.Lerp(0f, sound.volume, percent);
			yield return null; 
		}
		reset(sound);
	}

	IEnumerator fadeOut(Sound sound, float fadeTime) {
		float startVolume = sound.source.volume;
		float speed = 1 / fadeTime;
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * speed;
			sound.source.volume = Mathf.Lerp(startVolume, 0f, percent);
			yield return null;
		}
		reset(sound);
	}

	void reset(Sound s) {
		s.source.clip = s.clip;
		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
		s.source.loop = s.loop;
	}
}