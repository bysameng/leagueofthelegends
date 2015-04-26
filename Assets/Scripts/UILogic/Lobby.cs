using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class Lobby : MonoBehaviour {

	PlayerManager manager;
	List<UIText> enableMessages;
	bool lobbying = true;

	public System.Action onFinish;

	void Start () {
		GlobalSoundEffects.main.PlayClipAtPoint("bloop", Vector3.zero);
		manager = PlayerManager.main;
		enableMessages = new List<UIText>();
		UIDrawer.main.DrawText(new Vector3(0f, 2f, 0f), "PRESS A").textMesh.fontSize /= 2;
		for(int i = 0; i < manager.Players.Length; i++){
			enableMessages.Add(UIDrawer.main.DrawText(new Vector3(-manager.Players.Length/2f + i*2f, -1f, 0f), "-"));
		}
	}


	void Update () {
		if (!lobbying) return;
		int enabledCount = 0;
		for(int i = 0; i < manager.Players.Length; i++){
			bool isActive = manager.Players[i].isActive;
			enableMessages[i].text = isActive ? "X" : "-";
			enabledCount += isActive ? 1 : 0;
		}

		if (enabledCount == 2){
			lobbying = false;
			Invoke("StartGame", 1f);
		}
	}


	void StartGame(){
		UIDrawer.main.Clear();
		if (onFinish != null) onFinish();
		Destroy(this.gameObject);
	}


}
