using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	public static Director main;

	public PlayerManager playerManager{get; private set;}
	public GameLogic logic{get; private set;}

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
		PrefabManager.Instantiate("LobbyMenu").GetComponent<Lobby>().onFinish = OnFinishLobby;
	}

	void StopLobby(){
		playerManager.StopLobby();
	}

	void OnFinishLobby(){
		StopLobby();
		bool playerOneIsHuman = Random.value > .5f;
		playerManager.Players[0].playerType = playerOneIsHuman ? PlayerType.Human : PlayerType.Dog;
		playerManager.Players[1].playerType = playerOneIsHuman ? PlayerType.Dog : PlayerType.Human;
		DrawGame();
	}

	void DrawGame(){
		logic = new GameObject("GameLogic").AddComponent<GameLogic>();
		logic.StartGame();
	}



}
