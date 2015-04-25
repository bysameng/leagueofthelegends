using UnityEngine;
using System.Collections;
using InControl;

public enum PlayerType{Dog, Human, Unassigned};
public class Player {
	public InputDevice inputDevice{get; private set;}
	public PlayerType playerType = PlayerType.Unassigned;

	public void Reset(){
		inputDevice = null;
		playerType = PlayerType.Unassigned;
	}

	public void EnablePlayer(InputDevice device){
		inputDevice = device;
	}

}
