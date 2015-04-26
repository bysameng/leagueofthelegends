using UnityEngine;
using System.Collections;
using InControl;

public class HumanController : Controller {


	public int storedShits;
	int maxShit = 20;

	GameObject shits;

	protected RaycastHit hit;


	protected Leasher leasher;

	public Animator animator;

	float animating;

	public override void Start ()
	{
		base.Start ();
		prompt = PrefabManager.Instantiate("HumanPrompt", transform.position).GetComponent<ButtonPrompt>();
		leasher = PrefabManager.Instantiate("Leasher", transform.position).GetComponent<Leasher>();
		leasher.transform.parent = this.transform;

		shits = new GameObject("Shits");
		shits.transform.parent = this.transform;
		shits.transform.localPosition = Vector3.zero;
		shits.transform.rotation = Quaternion.identity;
	}

	protected override void DoLogic(){
		bool promptSet = false;;
		Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
		for(int i = 0; i < colliders.Length; i++){
			GameObject g = colliders[i].gameObject;
			Vector3 delta = g.transform.position - transform.position;
			if (Vector3.Dot(transform.forward, delta) < .2f){
				continue;
			}
			if (g.tag == "Dog"){
				prompt.SetPosition(gameObject);
				prompt.target = g;
				promptSet = true;
			}
		}
		if (!promptSet) prompt.ClearPosition();

		ButtonInput();

		if (animating > 0){
			animating -= Time.deltaTime;
			currentInput = Vector3.zero;
			return;
		}

		if (movement.magnitude > .1f){
			animator.SetFloat("Run", 1f);
			animator.speed = movement.magnitude;
		}
		else animator.SetFloat("Run", 0f);
	}


	void ButtonInput(){

		GameObject target = prompt.target;
		if (target == null) return;

		if (inputDevice.Action1.WasPressed){

			//leash dog
			if (target.tag == "Dog"){
				Debug.Log("leashed dog");
				GlobalSoundEffects.main.PlayClipAtPoint("leash", target.transform.position, .8f);
				leasher.target = target;
				target.tag = "LeashedDog";
			}

			prompt.FlashColor();

		}

		if (inputDevice.Action1.WasPressed){
			animator.SetFloat("Run", 0f);
			animating = 2f;
			animator.speed = 2;
			animator.SetTrigger("BendOver");
		}
	}


	void PickUpShit(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
		for(int i = 0; i < colliders.Length; i++){
			GameObject g = colliders[i].gameObject;
			if (g.tag != "Shit") continue;
			Vector3 delta = g.transform.position - transform.position;
			if (Vector3.Dot(transform.forward, delta) > .5f){
				if (storedShits < maxShit){
					storedShits++;
					g.transform.parent = shits.transform;
					g.transform.localPosition = Vector3.zero;
				}
			}
		}
	}



}
