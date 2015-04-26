using UnityEngine;
using System.Collections;

public class ShitPile : MonoBehaviour {

	private Vector3Damper sizeDamper;


	void Awake(){
		float size = Random.Range(.05f, .2f);
		sizeDamper = new Vector3Damper(new Vector3(size, size, size), .2f);
		transform.localScale = Vector3.zero;
	}


	void Update(){
		transform.localScale = sizeDamper.Value;
	}

	void OnDestroy(){
		SmoothDamper.main.RemoveDamper(sizeDamper);
	}


}
