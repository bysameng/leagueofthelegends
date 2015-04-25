using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	Vector3Damper positionDamper;
	ColorFader fader;

	public void Flash(Color color){
		fader.SetCurrentRenderColor(color);
	}

	public void Flash(){
		Flash (Color.white);
	}

	public void SetPosition(Vector3 position){
		positionDamper.Target = position;
	}

	void Awake(){
		positionDamper = new Vector3Damper(transform.position, .1f);
		fader = gameObject.AddComponent<ColorFader>();
		fader.fullColor = new Color(1f, 1f, 1f, .3f);
		fader.forceFullColor = true;
	}

	void Update(){
		transform.position = positionDamper.Value;
	}

	void OnDestroy(){
		SmoothDamper.main.RemoveDamper(positionDamper);
	}
}
