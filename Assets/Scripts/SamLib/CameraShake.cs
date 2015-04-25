using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	public static CameraShake main;
	
	public float shakeDuration = 0;
	public float shakeAmplitude = .1f;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 targetPosition;
	
	private Quaternion targetRotation;
	private FloatDamper fovDamper;
	
	void Awake(){
		main = this;
	}
	
	void Start(){
		fovDamper = new FloatDamper(12f, .8f);
	}
	
	public void Shake(float duration, float strength){
		if (shakeDuration < duration)
			shakeDuration = duration;
		if (shakeAmplitude < strength)
			shakeAmplitude = strength;
		fovDamper.Value -= Random.Range(strength, strength*2.2f);
	}
	
	public void LateUpdate(){
		if (shakeDuration > 0){
			targetPosition = GetRandomVec();
			shakeDuration -= Time.deltaTime;
			targetRotation = Quaternion.Euler(new Vector3(0, Random.Range(-shakeAmplitude*6f, shakeAmplitude*6f), 0f));
			shakeAmplitude -= Time.deltaTime;
			if (shakeDuration <= 0){
				targetPosition = Vector3.zero;
				shakeAmplitude = 0;
				targetRotation = Quaternion.identity;
			}
		}
		
		if (Vector3.Distance(transform.localPosition, targetPosition) > .01f)
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, shakeDuration > 0 ? .02f : .5f);
		
		transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 360 * Time.deltaTime);
		Camera.main.fieldOfView = fovDamper.Value;
	}
	
	private float GetRandomVal(){
		return Random.Range(-shakeAmplitude, shakeAmplitude);
	}
	
	private Vector3 GetRandomVec(){
		return new Vector3(GetRandomVal(), GetRandomVal()/2f, GetRandomVal());
	}
	
}
