using UnityEngine;
using System.Collections;
using InControl;

public class HumanController : Controller {


	protected RaycastHit hit;


	public override void Start ()
	{
		base.Start ();
		prompt = PrefabManager.Instantiate("HumanPrompt", transform.position).GetComponent<ButtonPrompt>();
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
				promptSet = true;
			}
		}
		if (!promptSet) prompt.ClearPosition();
	}



}
