using UnityEngine;
using System.Collections;

public class ScreenShakeCollisions : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		Vector3 vel = other.relativeVelocity;
		CameraShake.main.Shake(vel.magnitude/500f, vel.magnitude/300f);
	}
}
