using UnityEngine;
using System.Collections;

public class TextWidth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(.03f * Time.deltaTime, 0f, 0f);
	}
}
