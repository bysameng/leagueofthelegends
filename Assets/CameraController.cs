using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public static CameraController main;

	public GameObject objectToTrack;
	public Vector3 offset;

	void Awake(){
		main = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (objectToTrack != null){
			transform.position = objectToTrack.transform.position + offset;
			transform.LookAt(objectToTrack.transform.position);
		}
	}
}
