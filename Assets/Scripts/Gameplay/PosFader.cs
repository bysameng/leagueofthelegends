using UnityEngine;
using System.Collections;

public class PosFader : MonoBehaviour {

	public Vector3 targetPos;

	public Vector3 offset;
	public float speed = 2f;

	void Start(){
		targetPos = transform.localPosition;
		transform.localPosition += offset;
	}


	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
	}
}
