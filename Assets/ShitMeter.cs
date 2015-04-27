using UnityEngine;
using System.Collections;

public class ShitMeter : MonoBehaviour {

	float maxSize = 10;

	Vector3Damper scaleDamper;
	ColorFader fader;
	new Light light;

	public string text = "S    H    I    T            M    E    T    E    R";


	public Color color;


	/// <summary>
	/// Sets the shit. [0,1]
	/// </summary>
	/// <param name="shit">Shit.</param>
	public void SetShit(float shit){
		Debug.Log(shit);
		scaleDamper.Target = new Vector3(shit * maxSize, 1f, 1f);
		fader.fullColor = Color.Lerp(Color.black, color * 100f * shit, shit);
		light.color = color;
		light.intensity = shit*2f;
	}


	void Awake(){
		scaleDamper = new Vector3Damper(Vector3.one, .1f);
		fader = GetComponent<ColorFader>();
		light = GetComponent<Light>();
		SetShit(0);
	}


	void Update(){
		transform.localScale = scaleDamper.Value;
	}


}
