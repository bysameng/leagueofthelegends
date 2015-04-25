using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;


public class EffectsHandler : MonoBehaviour {
	
	public static EffectsHandler main;
	
	public Camera mainCamera;
	private Vector3 velocity = Vector3.zero;
	private static float smoothTime = .08f;
	private static float shakeIntensity;
	
	private static float shakeDuration;
	
	private bool frictioning;
	private float timeTarget;
	private float timeVelocity;
	private float timeSmoothInTime;
	private float timeSmoothOutTime;
	private float timeSmoothTime;
	private float timeSlowDuration;
	private AudioSource[] audioSources;
	
	
	private float abberationIntensity;
	private float abberationDuration;
	
	private VignetteAndChromaticAberration vignetteController;
	private float chromSmoothTime;
	private float chromVelocity;
	private float chromTarget;
	public bool chrom;
	
	private bool chromaticEnabled = true;
	private bool vignetteEnabled = true;
	private bool blurEnabled = true;
	private bool bloomEnabled = true;

	void Awake(){
		EffectsHandler.main = this;
	}
	
	void Start(){
		if (mainCamera == null)
			mainCamera = Camera.main;
		chromTarget = 0;
		chromSmoothTime = .2f;
		vignetteController =  mainCamera.GetComponent<VignetteAndChromaticAberration>();
	}
	
	public void ResetCamera(){
		//	private float xMin, xMax, yMin, yMax, strength, duration;
		shakeDuration = shakeIntensity = 0;
	}
	
	public void SetTimeScale(float timeScale){
		timeScale = Mathf.Clamp(timeScale, 0, 2);
		Time.timeScale = timeScale;
		SetAudioPitch(Time.timeScale);
	}
	
	void FindAllAudioSources(){
		audioSources = FindObjectsOfType<AudioSource>();
	}
	
	
	void SetAudioPitch(float pitchScale){
		pitchScale = Mathf.Clamp(pitchScale, .1f, 1.5f);
		if (audioSources != null)
		for(int i = 0; i < audioSources.Length; i++){
			if(audioSources[i] != null && audioSources[i].tag != "Director")
				audioSources[i].pitch = pitchScale;
		}
	}
	
	void LateUpdate(){
		UpdateTimeScale();
		if (shakeDuration > 0){
			shakeDuration -= Time.deltaTime;
			shakeIntensity -= (shakeIntensity / 1000) * Time.deltaTime;
			//			targetPosition += new Vector3(Random.Range(-shakeIntensity, shakeIntensity), 0, Random.Range(-shakeIntensity, shakeIntensity));
			if (chrom)
				chromTarget = 150f * shakeIntensity;
			if (Random.value > .5f) chromTarget *= -1;
		}
		else{
			shakeIntensity = shakeDuration = 0;
			if (chromTarget > 0){
				chromTarget -=Time.deltaTime*530f;
				if (chromTarget <0) chromTarget = 0;
			}
			else {chromTarget += Time.deltaTime*530f;
				if (chromTarget >0) chromTarget = 0;
			}
		}
		chromTarget = Mathf.Clamp(chromTarget, -90f, 90f);
		if (chromTarget == 0){
			chromSmoothTime = .35f;
			chrom = false;
		}
		else chromSmoothTime = .19f;
		
		if (chromaticEnabled){
			if (vignetteController != null) { 
				float value = Mathf.SmoothDamp(vignetteController.chromaticAberration, chromTarget, ref chromVelocity, chromSmoothTime);
				if (value < .001f && value > -.001f) value = 0f;
				vignetteController.chromaticAberration = Mathf.Clamp(value, -75f, 75f);
			}
		}
		
	}
	
	void UpdateTimeScale(){
		if (timeSlowDuration > 0){
			timeSlowDuration -= Time.unscaledDeltaTime;
			if (timeSlowDuration <= 0){
				timeTarget = 1f;
				timeSmoothTime = timeSmoothOutTime;
			}
		}
		else if (timeSlowDuration <= 0){
			timeTarget = 1f;
		}
		
		
		if (Mathf.Abs(Time.timeScale - timeTarget) >= .01f){
			//			Debug.Log("Current: " + Time.timeScale + " towards: " + timeTarget);
			SetTimeScale(Mathf.SmoothDamp(Time.timeScale, timeTarget, ref timeVelocity, timeSmoothTime, Mathf.Infinity, Utilities.unscaledDeltaTime));
		}
		else Time.timeScale = timeTarget;
	}
	
	public static void ShakeCamera(float strength, float duration, float smooth, bool chrom){
		ShakeCamera(strength, duration, smooth);
		EffectsHandler.main.chrom = chrom;
	}
	
	public static void ShakeCamera(float strength, float duration, float smooth){
		if (strength <= 0) return;
		shakeDuration = duration;
		//shakeIntensity = (shakeIntensity > strength) ? shakeIntensity : strength;
		shakeIntensity = strength;
		shakeIntensity = Mathf.Clamp(shakeIntensity, 0, 10f);
		smoothTime = smooth;
		CameraShake.main.Shake(duration, strength);
	}
	
	public void TimeSlow(float smoothInTime, float targetTime, float duration, float smoothOutTime){
		FindAllAudioSources();
		frictioning = true;
		targetTime = Mathf.Clamp(targetTime, .001f, 2f);
		timeTarget = targetTime;
		this.timeSmoothInTime = smoothInTime;
		this.timeSmoothOutTime = smoothOutTime;
		this.timeSmoothTime = smoothInTime;
		if (timeSlowDuration < 0) timeSlowDuration = 0;
		if (timeSlowDuration < duration)
			timeSlowDuration = duration;
	}
	

	public void Friction(float speed, float duration, float smoothTime, float smoothOutTime){
		//		StartCoroutine(Frictioner(speed, duration, smoothTime, smoothOutTime));
		TimeSlow(smoothTime, speed, duration, smoothOutTime);
		frictioning = true;
	}
	
	
	
	
	
	
}
