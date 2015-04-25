using UnityEngine;
using System.Collections;
using InControl;

public enum PlayerType{Dog, Human};
public class Player {
	public InputDevice inputDevice{get; set;}
	public PlayerType playerType;
}
