using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public static Director main;

	public PlayerManager playerManager{get; private set;}

	void Awake(){
		main = this;
		playerManager = gameObject.AddComponent<PlayerManager>();
	}

	void Start(){
		playerManager.StartLobby();
	}

	void DrawGame(){

	}


}
