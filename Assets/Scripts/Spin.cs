using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	public float speed = 1.0F;
	public float yOffset = 5.0f;
	
	// Animation properties to Lerp
	private Vector3 startMarker;
	private Vector3 endMarker;
	private Vector3 startRot = Vector3.zero;
	private Vector3 endRot = new Vector3(0, 360f, 0);
	
	private Vector3 startScale = Vector3.zero;
	private Vector3 endScale; 
	
	private float startTime;
	private float journeyLength;
	
	private bool finished = false;
	
	public bool lerpScale = true;		
	
	void Start() 
	{
		// Set up beginning scale
		endScale = transform.localScale;
		transform.localScale = Vector3.zero;
		
		startMarker = transform.position;
		
		startTime = Time.time;
		endMarker = new Vector3 (transform.position.x, transform.position.y + yOffset, transform.position.z);
		
		journeyLength = Vector3.Distance(startMarker, endMarker);
		
	}
	
	void Update() 
	{
		if (!finished)
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
			
			transform.eulerAngles = Vector3.Lerp(startRot, endRot, fracJourney);
			
			if (lerpScale)
				transform.localScale = Vector3.Lerp (startScale, endScale, fracJourney);
			
			if (transform.position == endMarker)
			{
				finished = true;		
			}
		}
		
	}
	
}
