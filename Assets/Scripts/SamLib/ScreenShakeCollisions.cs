using UnityEngine;
using System.Collections;

public class ScreenShakeCollisions : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		Vector3 vel = other.relativeVelocity;
		CameraShake.main.Shake(vel.magnitude/500f, vel.magnitude/300f);
		GlobalSoundEffects.main.PlayClipAtPoint("blockhit", transform.position, vel.magnitude/100f).pitch = Random.Range(.8f, 1.1f);
	}
}
