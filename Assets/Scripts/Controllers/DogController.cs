using UnityEngine;
using System.Collections;

public class DogController : Controller {

	public Animator animator;

	float shitSpeed = 1f;
	public float StoredShit{get; private set;}
	private bool shitting = false;

	private float shitCooldown = .5f;
	private float shitCooldownTimer;

	private float poopDelay = .1f;
	private float poopDelayTimer;

	private ColorFader fader;

	private ShitPile currentShitPile;


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

		if (inputDevice.Action1.IsPressed){
			currentInput = Vector3.zero;
		}

		if (inputDevice.Action1.IsPressed && !shitting && movement.magnitude < 1f){
			shitting = true;
			Poop();
		}


		if (shitting && inputDevice.Action1.IsPressed){
			if (poopDelayTimer > 0){
				poopDelayTimer -= Time.deltaTime;
			}
			else{
				Poop();
				poopDelayTimer = poopDelay;
			}
//			currentShitPile.magnitude += shitSpeed * Time.deltaTime;
			currentInput = Vector3.zero;
			fader.fullColor = Color.white;
		}
		else if (shitting && !inputDevice.Action1.IsPressed){
			shitting = false;
			fader.fullColor = Color.black;
			shitCooldownTimer = shitCooldown;
		}


		animator.speed = movement.magnitude;
	}

	void Poop(){
		currentShitPile = ShitManager.main.SpawnShit(transform.position - transform.forward/6f);
	}

}
