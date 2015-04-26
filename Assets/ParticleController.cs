using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	public static ParticleController main;

	public ParticleSystem sparks;

	void Awake(){
		main = this;
	}

	public void ExplodeSparks(int emission, Vector3 position){
		transform.position = position;
		sparks.Emit(emission);
	}


}