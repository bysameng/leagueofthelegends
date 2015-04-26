using UnityEngine;
using System.Collections;
using SamCinema;

public class TitleScreen : MonoBehaviour {

	UIText text;
	ColorFader fader;

	// Use this for initialization
	void Start () {
		Vignette v = new Vignette();

		v.AddAction(new WaitAction(2f));
		v.AddAction(new SoundAction("inception", Vector3.zero));
		v.AddAction(new SamAction(
			()=>{
			text = UIDrawer.main.DrawText(Vector3.zero, "T    H    E            L    E    A    G    U    E           O    F            T    H    E            L    E    G    E    N    D    S");
			text.gameObject.AddComponent<TextWidth>();
			fader = text.gameObject.AddComponent<ColorFader>();
			fader.fullColor = Color.white;
			fader.forceFullColor = true;
		}
		));
		v.AddAction(new WaitAction(4f));
		v.AddAction(new SamAction(()=>{			
			fader.fullColor = new Color(0f,0f,0f,0f);
		}));

//		v.AddAction(new SamAction(()=>{UIDrawer.main.Clear();}));
		v.AddAction(new WaitAction(3f));
		v.AddAction(new SamAction(()=>{Application.LoadLevel(1);}));

		v.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
