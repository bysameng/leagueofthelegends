using UnityEngine;
using System.Collections;

public class ColorOscillator : MonoBehaviour {

	public float oscillationSpeed;
	public Color currentColor;


	void Update () {
		HSBColor hsb = new HSBColor(currentColor);
		float newH = hsb.h + oscillationSpeed * Utilities.unscaledDeltaTime;
		if (newH > 1){
			newH = 0;
		}
		if (newH < 0){
			newH = 1;
		}
		hsb.h = newH;
		currentColor = hsb.ToColor();
	}
}
