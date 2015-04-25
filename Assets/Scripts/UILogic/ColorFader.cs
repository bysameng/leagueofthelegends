using UnityEngine;
using System.Collections;

public class ColorFader : MonoBehaviour {

	public float fadeTime = .4f;
	public Color fullColor;
	private Vector4 fadeVelocity = Vector4.zero;
	public bool forceFullColor;
	public bool isHalo;
	public bool alphaFadeOnly;
	public float delay;
	public bool unScaledTime;
	private ColorOscillator colorOscillator;
	public bool oscillate;
	public float oscillateSpeed = .5f;
	public bool isSharedMaterial = false;
	public string colorString = "_Color";

	public ColorDamper colorDamper;


	private Material material;

	public void OscillateColor(float speed, Color startColor, bool unscaledDeltaTime){
		if (colorOscillator == null)
			colorOscillator = gameObject.AddComponent<ColorOscillator>();
		colorOscillator.currentColor = startColor;
		colorOscillator.oscillationSpeed = speed;
		fadeTime = .01f;
		oscillate = true;
		this.unScaledTime = unscaledDeltaTime;
	}

	public void OscillateColor(float speed, Color startColor){
		OscillateColor(speed, startColor, this.unScaledTime);
	}

	public Color GetCurrentRenderColor(){
		return material.GetColor(colorString);
	}

	public void SetCurrentRenderColor(Color color){
		material.SetColor(colorString, color);
		colorDamper.Value = color;
	}

	// Use this for initialization
	void Awake () {
		if (isHalo) colorString = "_TintColor";

		//set up material
		if (isSharedMaterial){
			material = GetComponent<Renderer>().sharedMaterial;
		}
		else material = GetComponent<Renderer>().material;

		//set up damper
		colorDamper = new ColorDamper(forceFullColor ? fullColor : GetCurrentRenderColor(), fadeTime);
		SmoothDamper.main.RemoveDamper(colorDamper);


		if (alphaFadeOnly){
			Color oldColor;
			if (isHalo) oldColor = material.GetColor("_TintColor");
			else oldColor = GetComponent<Renderer>().material.GetColor(colorString);
			fullColor.r = oldColor.r;
			fullColor.g = oldColor.g;
			fullColor.b = oldColor.b;
		}

		if (forceFullColor) return;

	}


	void Update(){
		if (delay > 0){
			if (unScaledTime){
				delay -= Utilities.unscaledDeltaTime;
			}
			else{
				delay -= Time.deltaTime;
			}
			colorDamper.enabled = false;
			return;
		}
		else colorDamper.enabled = true;

		if (oscillate){
			if (colorOscillator == null) {
				if (fullColor != Color.white){
					OscillateColor(oscillateSpeed, fullColor);
				}
				else
					OscillateColor(oscillateSpeed, Color.red);
			}
			fullColor = colorOscillator.currentColor;
		}

		colorDamper.Value = GetCurrentRenderColor();
		colorDamper.Target = fullColor;
		colorDamper.Speed = fadeTime;
		colorDamper.isUnscaled = unScaledTime;
		colorDamper.Update();
		SetCurrentRenderColor(colorDamper.Value);
	}


	void OnDestroy(){
		if(oscillate){
			Destroy(colorOscillator);
		}
	}
}
