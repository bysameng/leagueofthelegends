using UnityEngine;
using System.Collections;

public class ShitPile : MonoBehaviour {

	void Awake(){
		float size = Random.Range(.05f, .2f);
		transform.localScale = new Vector3(size, size, size);
	}

}
