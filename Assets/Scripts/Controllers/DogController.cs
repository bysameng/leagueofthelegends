using UnityEngine;
using System.Collections;
using InControl;

public class DogController : MonoBehaviour {

	Vector3 currentInput;
	Vector3 movement;
	float maxSpeed = 2f;
	float acceleration = 40f;
	float dampingFactor = .5f;
	float slowDownFactor = .95f;


	new private Rigidbody rbody;
	private InputDevice inputDevice;


	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		if (inputDevice == null){
			if (InputManager.ActiveDevice != null){
				inputDevice = InputManager.ActiveDevice;
			}
			return;
		}
		CheckInput();
		CalculateMovement();
		DoMovement();
	}

	void CheckInput(){
		currentInput = new Vector3(inputDevice.Direction.X, 0, inputDevice.Direction.Y);
	}

	void CalculateMovement(){
		//accelerate
		movement += currentInput * acceleration * Time.deltaTime;

		//damping
		//damp more if no input
		float currentDampingFactor = currentInput == Vector3.zero ? slowDownFactor : dampingFactor;
		movement *= Mathf.Pow(1-currentDampingFactor, Time.deltaTime);

		//clamp maxspeed
		movement = Vector3.ClampMagnitude(movement, maxSpeed);
	}

	void DoMovement(){
		rbody.MovePosition(rbody.position + movement * Time.deltaTime);
	}

}
