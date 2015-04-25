using UnityEngine;
using System.Collections;

public class UIDrawer : MonoBehaviour {
	public static UIDrawer main;


	public UIText DrawText(Vector3 position, string message){
		UIText text = PrefabManager.Instantiate("UIText", position).GetComponent<UIText>();
		text.textMesh.text = message;
		text.transform.parent = gameObject.transform;
		text.transform.localPosition = position;
		return text;
	}


}