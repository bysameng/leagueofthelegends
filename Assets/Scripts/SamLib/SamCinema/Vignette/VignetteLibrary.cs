using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SamCinema;

//this class holds all the reusable, generic vignettes you make
public static class VignetteLibrary {

	private static Dictionary<string, Vignette> vignetteDictionary = new Dictionary<string, Vignette>();

	public static Vignette GetVignette(string vignetteName){
		Vignette v = null;
		vignetteDictionary.TryGetValue(vignetteName, out v);
		if (v == null) Debug.LogError("Vignette " + vignetteName + " doesn't exist!");
		return v;
	}

	public static void AddVignette(string vignetteName, Vignette v){
		vignetteDictionary.Add(vignetteName, v);
	}


}
