using UnityEngine;
using System.Collections;

public class UIText : MonoBehaviour {

	public TextMesh textMesh;

	void Awake(){
		textMesh = GetComponent<TextMesh>();
	}



}
