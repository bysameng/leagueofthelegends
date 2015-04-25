using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public static Director main;

	public PlayerManager playerManager{get; private set;}

	GameObject lobby;

	void Awake(){
		main = this;
		playerManager = gameObject.AddComponent<PlayerManager>();
	}

	void Start(){
		StartLobby();
	}

	void StartLobby(){
		playerManager.StartLobby();
	}

	void StopLobby(){
		playerManager.StopLobby();
	}

	void DrawGame(){

	}



}
