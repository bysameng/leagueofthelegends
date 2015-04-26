using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SamCinema;

public delegate void SamActionDelegate();
public class SamCinemaManager : MonoBehaviour {
	public static SamCinemaManager main;

	private List<Vignette> vignettes;

	void Awake(){
		main = this;
		vignettes = new List<Vignette>();
	}


	void Update () {
		UpdateVignettes();
	}


	void UpdateVignettes(){
		for(int i = 0; i < vignettes.Count; i++){
			vignettes[i].Update();
			if (vignettes[i].IsFinished){
				vignettes.RemoveAt(i);
			}
		}
	}


	public void AddVignette(Vignette vignette){
		if (vignettes.Contains(vignette)) {Debug.LogError("Cannot add Vignette. Already exists."); return;}
		vignettes.Add(vignette);
		vignette.InternalPlay();
	}


	public void RemoveVignette(Vignette vignette){
		vignettes.Remove(vignette);
	}

	public bool IsRunning(Vignette v){
		return vignettes.Contains(v);
	}


}
