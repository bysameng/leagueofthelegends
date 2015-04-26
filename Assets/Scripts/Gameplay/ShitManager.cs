using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShitManager : MonoBehaviour {
	public static ShitManager main;

	public float shitCounter{get; private set;}

	private List<ShitPile> shits;

	void Awake(){
		main = this;
		shits = new List<ShitPile>();
	}

	public ShitPile SpawnShit(Vector3 position){
		Debug.Log("spawningshit");
		ShitPile shitPile = PrefabManager.Instantiate("ShitPile", position).GetComponent<ShitPile>();
		shits.Add(shitPile);
		return shitPile;
	}


	void Update(){
		UpdateShitCounter();
	}


	void UpdateShitCounter(){
		float counter = 0;
		for(int i = 0; i < shits.Count; i++){
			counter++;
		}
		shitCounter = counter;
	}




}
