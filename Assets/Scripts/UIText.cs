using UnityEngine;
using System.Collections;

public class UIText : MonoBehaviour {

	public TextMesh textMesh;
	public string text{get{return textMesh.text;} set{textMesh.text = value;}}

	void Awake(){
		textMesh = GetComponent<TextMesh>();
	}



}
