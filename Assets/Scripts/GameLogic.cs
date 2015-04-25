using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	PlayerManager manager = PlayerManager.main;

	public void StartGame(){
		SpawnLevel();
		foreach(var p in manager.Players){
			SpawnPlayer(p);
		}
	}

	void SpawnLevel(){
		PrefabManager.Instantiate("Level");
	}

	Controller SpawnPlayer(Player player){
		Vector3 position = new Vector3(3f, 0f, 0f);
		Vector3 spawnPos = player.playerType == PlayerType.Human ? -position : position;
		return SpawnPlayer(player, spawnPos);
	}

	Controller SpawnPlayer(Player player, Vector3 position){
		Debug.Log(player.playerType.ToString());
		Controller c = PrefabManager.Instantiate(player.playerType.ToString(), position).GetComponent<Controller>();
		c.SetPlayer(player);
		return c;
	}

}
