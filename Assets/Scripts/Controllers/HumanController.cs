using UnityEngine;
using System.Collections;
using InControl;

public class HumanController : Controller {


	protected RaycastHit hit;


	protected Leasher leasher;

	public Animator animator;

	public override void Start ()
	{
		base.Start ();
		prompt = PrefabManager.Instantiate("HumanPrompt", transform.position).GetComponent<ButtonPrompt>();
		leasher = PrefabManager.Instantiate("Leasher", transform.position).GetComponent<Leasher>();
		leasher.transform.parent = this.transform;
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

		animator.speed = movement.magnitude;
	}


	void ButtonInput(){
		GameObject target = prompt.target;
		if (target == null) return;

		if (inputDevice.Action1.WasPressed){

			//leash dog
			if (target.tag == "Dog"){
				Debug.Log("leashed dog");
				leasher.target = target;
				target.tag = "LeashedDog";
			}

			prompt.FlashColor();

		}

		if (inputDevice.Action1.WasPressed){

		}
	}



}
