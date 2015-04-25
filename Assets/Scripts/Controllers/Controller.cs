using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	protected Vector3 currentInput;
	protected Vector3 movement;
	protected float maxSpeed = 2f;
	protected float acceleration = 40f;
	protected float dampingFactor = .5f;
	protected float slowDownFactor = .95f;

	new protected Rigidbody rbody;
	protected Player player;
	protected InputDevice inputDevice{
		get{return player.inputDevice;}
	}

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
		rbody.isKinematic = false;
	}

	// Update is called once per frame
	void Update () {
		if (inputDevice == null) return;
		CheckInput();
		CalculateMovement();
		DoMovement();
	}

	protected virtual void CheckInput(){
		currentInput = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);
	}

	protected virtual void CalculateMovement(){
		//accelerate
		movement += currentInput * acceleration * Time.deltaTime;

		//damping
		//damp more if no input
		float currentDampingFactor = currentInput == Vector3.zero ? slowDownFactor : dampingFactor;
		movement *= Mathf.Pow(1-currentDampingFactor, Time.deltaTime);

		//clamp maxspeed
		movement = Vector3.ClampMagnitude(movement, maxSpeed);
	}

	protected void DoMovement(){
		rbody.MovePosition(rbody.position + movement * Time.deltaTime);
	}


}
