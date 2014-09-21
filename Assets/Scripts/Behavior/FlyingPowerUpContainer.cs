using UnityEngine;
using System.Collections;

public class FlyingPowerUpContainer : StationaryPowerUpContainer {

	public float 		xSpeed = 0.05f;

	void Awake(){
		this.open = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		// move in a sine wave
		Vector2 pos = transform.position;
		pos.x += xSpeed;
		pos.y = Mathf.Sin(Time.time) * 2;
		transform.position = pos;

	}
}
