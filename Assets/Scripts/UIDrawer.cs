using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIDrawer : MonoBehaviour {
	public static UIDrawer main;

	public GameObject children;

	void Awake(){
		main = this;
		SetUpChildrenObj();
	}

	void SetUpChildrenObj(){
		if (children != null) Destroy(children);
		children = new GameObject("UIObjects");
		children.transform.parent = this.transform;
		children.transform.localPosition = Vector3.zero;
		children.transform.localRotation = Quaternion.identity;
	}


	public UIText DrawText(Vector3 position, string message){
		UIText text = PrefabManager.Instantiate("UIText", position).GetComponent<UIText>();
		text.textMesh.text = message;
		text.transform.parent = children.transform;
		text.transform.localPosition = position;
		text.transform.localRotation = Quaternion.identity;
		return text;
	}

	public void Clear(){
		SetUpChildrenObj();
	}


}