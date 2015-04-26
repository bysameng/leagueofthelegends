using UnityEngine;
using System.Collections;

public class Leasher : MonoBehaviour {

	private GameObject _target;
	public GameObject target{get{return _target;} set{_target = value; durability = durabilityFull;}}
	float leashRadius = 2f;

	float durabilityFull = 5000f;
	float durability;

	LineRenderer lineRenderer;

	ColorFader fader;

	void Awake(){
		lineRenderer = GetComponent<LineRenderer>();
		fader = gameObject.AddComponent<ColorFader>();
	}


	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target == null ? transform.position : target.transform.position);

		if (target == null) return;

		Vector3 delta = transform.position - target.transform.position;

		if (delta.magnitude > leashRadius){
			Vector3 force = delta*100f;
			target.GetComponent<Rigidbody>().AddForce(force);
			GlobalSoundEffects.main.PlayClipAtPoint("stretch"+Random.Range(1, 8), transform.position, .4f);
			durability -= force.magnitude;
			if (durability <= 0){
				target.GetComponent<Rigidbody>().AddForce(-force*4f);

				ParticleController.main.ExplodeSparks(100, target.transform.position);
				GlobalSoundEffects.main.PlayClipAtPoint("snap", transform.position);

				if (target.tag == "LeashedDog")
					target.tag = "Dog";

				target = null;


			}
		}
		SetStrength();
	}

	void SetStrength(){
		fader.fullColor = Color.Lerp(Color.red, Color.black, durability/durabilityFull);
	}
}
