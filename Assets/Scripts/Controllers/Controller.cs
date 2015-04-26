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

	protected float rotationSpeed = 720;

	new protected Rigidbody rbody;
	protected Player player;
	protected InputDevice inputDevice{
		get{return player.inputDevice;}
	}

	protected InputControl aInput{get{return inputDevice.Action1;}}
	protected InputControl bInput{get{return inputDevice.Action2;}}

	protected ButtonPrompt prompt;


	// Use this for initialization
	public virtual void Start () {
		rbody = GetComponent<Rigidbody>();
		rbody.isKinematic = false;
	}

	// Update is called once per frame
	public virtual void Update () {
		if (inputDevice == null) return;
		CheckInput();
		DoLogic();
		CalculateMovement();
		DoMovement();
	}

	public void SetPlayer(Player player){
		this.player = player;
	}

	protected virtual void DoLogic(){}

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

	protected virtual void DoMovement(){
		rbody.MovePosition(rbody.position + movement * Time.deltaTime);
		if (movement.magnitude > .5f){
			rbody.MoveRotation(Quaternion.RotateTowards(Quaternion.LookRotation(transform.forward), Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime));
		}
	}


}
