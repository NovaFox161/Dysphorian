using UnityEngine;

[RequireComponent(typeof(ObjectState))]
public class TvScreen : Interactable {
	public GameObject screen;
	public bool loop;
	public bool autoPlay;
	public bool affectStats;
	public bool absoluteStats;
	public bool multiUse = true;
	public AudioClip movieAudio;

	public PlayerStats stats;

	AudioSource audioSource;
	ObjectState state;
	bool tvOn;

	public override void Start () {
		base.Start();
		state = gameObject.GetComponent<ObjectState>();

		if (GameManager.getManager().hasState(state.getId())) {
			state.update(GameManager.getManager().getState(state.getId()));
		}
		audioSource = screen.GetComponent<AudioSource>();
		Renderer r = screen.GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		if (autoPlay) {
			TurnOn();
		} else {
			r.material.color = Color.black;
		}
		movie.loop = loop;
		audioSource.loop = loop;

		GameManager.getManager().onPause += Pause;
		GameManager.getManager().onUnpause += UnPause;
	}

	void OnDestroy() {
		GameManager.getManager().onPause -= Pause;
		GameManager.getManager().onUnpause -= UnPause;
	}

	void Pause() {
		Renderer r = screen.GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;

		if (movie.isPlaying) {
			tvOn = true;
			movie.Pause();
			if (movieAudio != null) {
				audioSource.Pause();
			}
		}
	}

	void UnPause() {
		Renderer r = screen.GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		r.material.color = Color.white;

		if (!movie.isPlaying && tvOn) {
			tvOn = true;
			movie.Play();
			if (movieAudio != null) {
				audioSource.UnPause();
			}
		}
	}

	void TurnOn() {
		Renderer r = screen.GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		r.material.color = Color.white;

		if (!movie.isPlaying && !tvOn) {
			movie.Play();
			tvOn = true;
			state.setOn(true);
			state.setUsed(true);

			if (movieAudio != null) {
				audioSource.clip = movieAudio;
				audioSource.Play();
			}

			if (affectStats && !state.hasChangedStats()) {
				state.setChangedStats(true);
				if (absoluteStats) {
					GameManager.getManager().affectStats(stats);
				} else {
					GameManager.getManager().adjustStats(stats);
				}
			}
			GameManager.getManager().updateState(state);
		}
	}

	void TurnOff() {
		Renderer r = screen.GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;
		r.material.color = Color.black;

		if (movie.isPlaying) {
			movie.Stop();
			tvOn = false;
			state.setOn(false);
			state.setUsed(true);

			GameManager.getManager().updateState(state);

			if (movieAudio != null) {
				audioSource.Stop();
			}
		}
	}

	public override void Interact() {
		if (tvOn) {
			TurnOff();
		} else {
			if (state.hasBeenUsed()) {
				if (multiUse) {
					TurnOn();
				}
			} else {
				TurnOn();
			}
		}
	}
}