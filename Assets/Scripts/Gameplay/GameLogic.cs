using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	PlayerManager manager = PlayerManager.main;
	ShitManager shitManager;

	TextMesh timer;
	float t;

	bool playing = false;

	public AudioSource bg;


	public void StartGame(){
		Debug.Log("Starting gamelogic...");
		PrefabManager.Instantiate("Portraits", transform.position);
		Invoke("RealStart", 5f);
	}

	void RealStart(){

		if (shitManager != null) Destroy (shitManager);
		shitManager = gameObject.AddComponent<ShitManager>();


		SpawnLevel();
		foreach(var p in manager.Players){
			SpawnPlayer(p);
		}
		timer = PrefabManager.Instantiate("Timer").GetComponentInChildren<TextMesh>();
		timer.text = ""+t;
		playing = true;
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

	void Update(){
		if (playing){
			t += Time.deltaTime;
			timer.text = ""+t;
		}
	}

	public void GameOver(){
		bg.Stop();
		playing = false;
		GlobalSoundEffects.main.PlayClipAtPoint("endround", Vector3.zero, 1f);
		timer.gameObject.SetActive(false);
		UIText text = UIDrawer.main.DrawText(Vector3.zero, "YOU VALIANTLY SURVIVED FOR " + t + " SECONDS.");
		text.textMesh.fontSize = 70;
		Invoke("StartShitting", 3f);
	}

	void StartShitting(){
		GlobalSoundEffects.main.PlayClipAtPoint("inception", Vector3.zero, "BGM", 1f);
		GameObject dog = GameObject.FindGameObjectWithTag("Dog");
		if (dog == null)
			dog = GameObject.FindGameObjectWithTag("LeashedDog");

		dog.GetComponent<DogController>().endgamed = true;
		CameraController.main.objectToTrack = dog;
		CameraController.main.offset = new Vector3(0f, 4f, -4f);
		Invoke("Stop", 3f);
	}

	void Stop(){
		Application.LoadLevel(1);
	}

}
