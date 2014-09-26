using UnityEngine;
using System.Collections;

public class Lava : Boundary {

	private float 	lavaSpeed = 1.2f;

	void Start() {

	}

	void Update() {
		var pos = transform.position;
		pos.y += lavaSpeed * Time.deltaTime;
		transform.position = pos;
	}

	protected void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "BillRizer") {
			//LavaTracking script = (LavaTracking) Camera.main.GetComponent(typeof(LavaTracking));
			//script.ResetCamera();
		}

		OnTriggerExit2D (other);
	}
}
