using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager main;

	protected List<Player> players;
	public Player[] Players{get{return players.ToArray();}}

	protected bool lobbying = false;
	protected int playerCount = 2;

	public void StartLobby(){
		Debug.Log("Starting lobby");
		lobbying = true;
	}

	public void StopLobby(){
		lobbying = false;
	}

	void Awake(){
		main = this;
	}

	void Start(){
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
		InputDevice active = InputManager.ActiveDevice;
		if (active.Action1.WasPressed){

			//enable this device if it's not enabled.
			if (GetPlayer(active) == null){
				bool success = EnableDevice(active);
				if (success)
				Debug.Log("Enabled device.");
//				GlobalSoundEffects.main.PlayClipAtPoint("inception");
			}
		}

		//disable player if B is pressed
		else if (active.Action2.WasPressed){
			if (GetPlayer(active) != null){
				bool success = DisableDevice(active);
				if (success)
				Debug.Log("Disabled device.");
			}
		}
	}


	//enable this device 
	//returns true if successful attachment to a player
	bool EnableDevice(InputDevice device){
		//try to find a free player to attach to
		Player player = FindFreePlayer();

		//if found, let's attach it
		if (player != null){
			player.EnablePlayer(device);
			return true;
		}
		return false;
	}


	bool DisableDevice(InputDevice device){
		Player p = GetPlayer(device);
		if (p != null){
			p.Reset();
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
	Player GetPlayer(InputDevice device){
		for(int i= 0; i < players.Count; i++){
			if (players[i].inputDevice == device) return players[i];
		}
		return null;
	}

	
}
