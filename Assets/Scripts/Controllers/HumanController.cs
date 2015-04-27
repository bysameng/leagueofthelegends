using UnityEngine;
using System.Collections;
using InControl;

public class HumanController : Controller {


	public int storedShits;
	int maxShit = 80;

	int pooped = 0;

	GameObject shits;

	protected RaycastHit hit;


	protected Leasher leasher;

	public Animator animator;





	float animating;

	void Awake(){
		this.shitmeter = PrefabManager.Instantiate("ShitMeter", Vector3.zero, Quaternion.Euler(0, 90f, 0)).GetComponentInChildren<ShitMeter>();
	}


	public override void Start ()
	{
		base.Start ();
		maxSpeed *= 1.2f;
		prompt = PrefabManager.Instantiate("HumanPrompt", transform.position).GetComponent<ButtonPrompt>();
		leasher = PrefabManager.Instantiate("Leasher", transform.position).GetComponent<Leasher>();
		leasher.transform.parent = this.transform;

		shits = new GameObject("Shits");
		shits.transform.parent = this.transform;
		shits.transform.localPosition = Vector3.zero;
		shits.transform.rotation = Quaternion.identity;
	}

	protected override void DoLogic(){
		shitmeter.SetShit(storedShits*1f / maxShit*1f);
		bool promptSet = false;;
		Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
		for(int i = 0; i < colliders.Length; i++){
			if (colliders[i].attachedRigidbody == null) continue;
			GameObject g = colliders[i].attachedRigidbody.gameObject;
			Vector3 delta = g.transform.position - transform.position;
			if (Vector3.Dot(transform.forward, delta) < .2f){
				continue;
			}
			if (g.tag == "Dog"){
				prompt.SetPosition(gameObject, "L    E    A    S    H");
				prompt.target = g;
				promptSet = true;
			}

			if (g.tag == "Can" && storedShits > 0){
				prompt.SetPosition(gameObject, "T    R    A    S    H");
				prompt.target = g;
				promptSet = true;
			}
		}

		if (!promptSet) prompt.ClearPosition();


		if (animating > 0){
			animating -= Time.deltaTime;
			currentInput = Vector3.zero;
			animator.SetFloat("Run", 0f);
			animator.speed = 2f;
			return;
		}

		ButtonInput();

		if (movement.magnitude > .1f){
			animator.SetFloat("Run", 1f);
			animator.speed = movement.magnitude;
		}
		else animator.SetFloat("Run", 0f);
	}


	void ButtonInput(){

		if (inputDevice.Action1.WasPressed){

			GameObject target = prompt.target;
			if (target != null){

				//leash dog
				if (target.tag == "Dog"){
					Debug.Log("leashed dog");
					GlobalSoundEffects.main.PlayClipAtPoint("leash", target.transform.position, .8f);
					leasher.target = target;
					target.tag = "LeashedDog";
					DogController d = target.GetComponent<DogController>();
					d.StoredShit /= 4f;
					d.StoredShit = Mathf.Clamp(d.StoredShit, 0, 10);
				}

				if (target.tag == "Can"){
					Debug.Log("Trashing shit");
					target.GetComponent<ColorFader>().SetCurrentRenderColor(Color.white * 10f);
					GlobalSoundEffects.main.PlayClipAtPoint("trash", target.transform.position, .9f);
					TrashShit();
				}

				prompt.FlashColor();
			}

			if (PickUpShit()){
				GlobalSoundEffects.main.PlayClipAtPoint("bag-grab", transform.position, .7f, 1.1f, .8f);
				animator.SetFloat("Run", 0f);
				animating = .2f;
				animator.speed = 2;
				animator.SetTrigger("BendOver");
			}
		}


		if (inputDevice.Action1.WasPressed){
			animator.SetFloat("Run", 0f);
			animating = 1f;
			animator.speed = 2;
			animator.SetTrigger("BendOver");
		}
	}




	bool PickUpShit(){
		Debug.Log("pickupshit");
		bool foundShit = false;
		Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
		for(int i = 0; i < colliders.Length; i++){
			if (colliders[i].attachedRigidbody == null) continue;
			GameObject g = colliders[i].attachedRigidbody.gameObject;
			Debug.Log(g.name);
			if (g.tag == "Shit") {
				foundShit = true;
				Debug.Log("fuck");
				Vector3 delta = g.transform.position - transform.position;
				if (Vector3.Dot(transform.forward, delta) > .1f){
					if (storedShits < maxShit){
						storedShits++;
						pooped++;
						g.transform.parent = shits.transform;
						g.transform.localPosition = Vector3.zero;
						g.SetActive(false);
					}
				}
			}
		}
		return foundShit;
	}




	bool TrashShit(){
		if (storedShits == 0){
			return false;
		}

		if (storedShits > maxShit / 3f){
			Debug.Log("fuck youi dammit");
			if (Random.value > .5f)
				GlobalSoundEffects.main.PlayClipAtPoint("golfclap", transform.position, "BGM", 1f);
			else
				GlobalSoundEffects.main.PlayClipAtPoint("gasp" + Random.Range(1, 3), transform.position, "BGM", 1f);

		}

		storedShits = 0;
		return true;
	}



}
