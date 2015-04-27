using UnityEngine;
using System.Collections;

public class Portraits : MonoBehaviour {

	public PosFader[] faders;

	// Use this for initialization
	void Start () {
		AudioSource s = GlobalSoundEffects.main.PlayClipAtPoint("swoosh", Vector3.zero, 1f);
		s.Stop();
		s.PlayDelayed(.2f);
		faders = GetComponentsInChildren<PosFader>();
		Invoke("FadeOff", 2f);
	}


	void FadeOff(){

//		Director.main.logic.bg = GlobalSoundEffects.main.PlayClipAtPoint("legendleague", Vector3.zero, "BG", .5f);
		Director.main.logic.bg.pitch = 1f;

		for(int i = 0; i < faders.Length; i++){
			faders[i].targetPos += faders[i].offset * 2f;
		}
	}
}
