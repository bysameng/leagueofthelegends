using UnityEngine;
using System.Collections;

public class DogController : Controller {

	public Animator animator;

	float shitSpeed = .9f;
	public float StoredShit{get; set;}
	int shitsAvailable{get{return (int)StoredShit;}}

	float maxShit = 60;

	private bool shitting = false;

	private float shitCooldown = .5f;
	private float shitCooldownTimer;

	private float poopDelay = .1f;
	private float poopDelayTimer;

	private ColorFader fader;

	private ShitPile currentShitPile;

	public bool endgamed = false;


	void Awake(){
		this.shitmeter = PrefabManager.Instantiate("ShitMeter", Vector3.zero, Quaternion.Euler(0, -90f, 0)).GetComponentInChildren<ShitMeter>();
	}

	public override void Start(){
		base.Start();
		maxSpeed *= 1.6f;
		acceleration *= 1.6f;
		fader = gameObject.AddComponent<ColorFader>();
		fader.colorString = "_EmissionColor";
		fader.fullColor = Color.black;
	}


	protected override void DoLogic(){
		if (endgamed){
			if (poopDelayTimer > 0){
				poopDelayTimer -= Time.deltaTime;
			}
			else{
				Poop();
				poopDelayTimer = poopDelay / shitSpeed;
			}
			poopDelay -= Time.deltaTime / 10f;
		}
		shitmeter.SetShit(Mathf.Clamp(StoredShit*1f / maxShit*1f, 0f, 1f));
		base.DoLogic();

		StoredShit += Time.deltaTime * shitSpeed;
		shitSpeed += Time.deltaTime * .1f;

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

		if (inputDevice.Action2.IsPressed && !shitting && movement.magnitude < 1f){
			shitting = true;
			StoredShit *= .66f;
			if (StoredShit > 10){
				GlobalSoundEffects.main.PlayClipAtPoint("gasp1", transform.position, "BGM", 1f);
			}
			rbody.AddForce(transform.forward * StoredShit * 10f);
			for(int i = 0; i < shitsAvailable; i++){
				ShitPile shitPile = PrefabManager.Instantiate("ShitPile", transform.position - transform.forward/6f).GetComponent<ShitPile>();
				shitPile.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere);
				Poop();
			}
			poopDelayTimer = poopDelay/2f;
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
		if (shitsAvailable > 0 || endgamed){
			StoredShit -= 1f;
			currentShitPile = ShitManager.main.SpawnShit(transform.position - transform.forward/6f);
			GlobalSoundEffects.main.PlayClipAtPoint("poot", transform.position, .2f, 1.2f, .6f);
			if (endgamed) currentShitPile.transform.localScale *= 2f;
		}
	}


}
