using UnityEngine;
using System.Collections;

namespace SamCinema{
	public class SoundAction : SamAction {
		public AudioClip audioClip;
		public SoundAction(AudioClip clip, Vector3 positionToPlay, bool waitFor = false){
			this.audioClip = clip;
			AudioSource s = null;
			OnStart = ()=>{s = GlobalSoundEffects.main.PlayClipAtPoint(audioClip, positionToPlay);};
			if (waitFor) WaitFor = ()=>{return !s.isPlaying;};
		}
		public SoundAction(AudioClip clip, Vector3 positionToPlay, float time) : this(clip, positionToPlay){
			this.Timer = time;
		}
		public SoundAction(string clip, Vector3 positionToPlay, bool waitFor = false){
			AudioSource s = null;
			OnStart = ()=>{
				s = GlobalSoundEffects.main.PlayClipAtPoint(clip, positionToPlay);
				if (s != null) audioClip = s.clip;};
			if (waitFor) WaitFor = ()=>{return !s.isPlaying;};
		}
		public SoundAction(string clip, Vector3 positionToPlay, float time) : this(clip, positionToPlay){
			this.Timer = time; 
		}

		public SoundAction(AudioClip clip, AudioSource audioSource, bool waitFor = false){
			OnStart = ()=>{
				audioSource.clip = clip;
				audioSource.Play();
			};
			if (waitFor) WaitFor = ()=>{return !audioSource.isPlaying;};
		}
		public SoundAction(AudioClip clip, AudioSource audioSource, float timer) : this(clip, audioSource, false){
			this.Timer = timer;
		}

	}


}
