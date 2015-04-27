using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShitManager : MonoBehaviour {
	public static ShitManager main;

	public int shitCounter{get; private set;}
	int shitsToWin = 70;


	private ShitMeter shitmeter;

	private List<ShitPile> shits;

	bool canwin = true;

	void Awake(){
		main = this;
		shits = new List<ShitPile>();
		shitmeter = PrefabManager.Instantiate("ShitMeter").GetComponentInChildren<ShitMeter>();
	}


	public ShitPile SpawnShit(Vector3 position){
		ShitPile shitPile = PrefabManager.Instantiate("ShitPile", position).GetComponent<ShitPile>();
		shits.Add(shitPile);
		return shitPile;
	}



	void Update(){
		UpdateShitCounter();
	}


	void UpdateShitCounter(){
		int counter = 0;
		for(int i = 0; i < shits.Count; i++){
			if (shits[i].gameObject.activeSelf)
				counter++;
		}
		shitCounter = counter;
		shitmeter.SetShit(shitCounter/((float)shitsToWin));

		if (shitCounter >= shitsToWin && canwin){
			canwin = false;
			Director.main.logic.GameOver();
		}
	}




}
