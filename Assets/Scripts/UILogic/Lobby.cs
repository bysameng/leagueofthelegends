using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class Lobby : MonoBehaviour {

	PlayerManager manager;

	List<GameObject> enableMessages;



	void Start () {
		manager = PlayerManager.main;
		enableMessages = new List<GameObject>();
		for(int i = 0; i < manager.Players.Length; i++){

		}
	}

	
	void Update () {
		for(int i = 0; i < manager.Players.Length; i++){
		}
	}






}
