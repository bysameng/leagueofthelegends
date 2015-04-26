using UnityEngine;
using System.Collections;

public class ButtonPrompt : MonoBehaviour {

	Vector3Damper positionDamper;
	public ColorFader fader;

	private readonly Vector3 hoverBelowPos = new Vector3(0f, -1f, 0f);
	private readonly Vector3 offset = new Vector3(0f, 1.5f, 0f);

	private bool setPos = false;

	public GameObject target;

	void Awake(){
		transform.rotation = Camera.main.transform.rotation;
		positionDamper = new Vector3Damper(transform.position, .1f);
//		fader = gameObject.AddComponent<ColorFader>();
		fader.fadeTime = .1f;
		fader.forceFullColor = true;
		fader.fullColor = new Color(1f,1f,1f,0f);
	}

	public void SetPosition(GameObject objectToHover){
		if (!setPos){
			positionDamper.Value = objectToHover.transform.position + hoverBelowPos;
			setPos = true;
		}
		positionDamper.Target = objectToHover.transform.position + offset;
		fader.fullColor = new Color(1f, 1f, 1f, .8f);
	}

	public void ClearPosition(){
		target = null;
		fader.fullColor = new Color(1f,1f,1f,0f);
		positionDamper.Target = transform.position + hoverBelowPos;
		setPos = false;
	}

	// Update is called once per frame
	void Update () {
		transform.position = positionDamper.Value;
	}

	public void FlashColor(Color color){
		fader.fullColor = color;
	}

	public void FlashColor(){
		fader.fullColor = Color.blue;
	}



}
