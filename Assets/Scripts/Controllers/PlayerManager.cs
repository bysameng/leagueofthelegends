using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager main;

	protected bool lobbying;
	protected List<Player> players;
	protected int playerCount = 2;

	public void StartLobby(){
		lobbying = true;
	}

	public void StopLobby(){
		lobbying = false;
	}

	void Awake(){
		main = this;
	}

	void Start(){
		lobbying = false;
		SetupPlayers();
	}

	//setup players and controllers
	void SetupPlayers(){
		players = new List<Player>();
		for(int i = 0; i < playerCount; i++){
			players.Add(new Player());
		}
	}


	void Update () {
		if (lobbying){
			LobbyUpdate();
		}
	}

	//check for devices
	void LobbyUpdate(){
		//check for input on all devices
		if (InputManager.ActiveDevice.Action1.WasPressed){
			InputDevice active = InputManager.ActiveDevice;

			//enable this device if it's not enabled.
			if (!DeviceEnabled(active))
				EnableDevice(active);
		}
	}


	//enable this device 
	//returns true if successful attachment to a player
	bool EnableDevice(InputDevice device){
		//try to find a free player to attach to
		Player player = FindFreePlayer();

		//if found, let's attach it
		if (player != null){
			player.inputDevice = device;
			return true;
		}
		return false;
	}


	//return a player that doesn't have a device
	Player FindFreePlayer(){
		for(int i = 0; i < players.Count; i++){
			if (players[i].inputDevice == null){
				return players[i];
			}
		}
		//return null if all players are taken
		return null;
	}


	//check if a device is already used
	bool DeviceEnabled(InputDevice device){
		for(int i= 0; i < players.Count; i++){
			if (players[i].inputDevice == device) return true;
		}
		return false;
	}
}
