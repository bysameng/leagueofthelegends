using UnityEngine;
using System.Collections;

public class DogController : Controller {

	public float StoredShit{get; private set;}
	private bool shitting = false;

	private float shitCooldown = .5f;
	private float shitCooldownTimer;

	private ColorFader fader;


	public override void Start(){
		base.Start();
		maxSpeed *= 1.6f;
		acceleration *= 1.6f;
		fader = gameObject.AddComponent<ColorFader>();
		fader.colorString = "_EmissionColor";
		fader.fullColor = Color.black;
	}


	protected override void DoLogic(){
		base.DoLogic();
		if (shitCooldownTimer > 0){
			shitCooldownTimer -= Time.deltaTime;
			currentInput = Vector3.zero;
		}

		if (aInput.IsPressed && movement.magnitude < 1f){
			Debug.Log("Shitting");
			shitting = true;
			shitCooldownTimer = shitCooldown;
		}

		if (shitting && !aInput.IsPressed){
			shitting = false;
		}

		if (shitting){
			currentInput = Vector3.zero;
			fader.fullColor = Color.red;
		}
		else{
			fader.fullColor = Color.black;
		}
	}

}
