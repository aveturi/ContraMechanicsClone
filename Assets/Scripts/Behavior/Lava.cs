using UnityEngine;
using System.Collections;

public class Lava : Boundary {

	private float 	lavaSpeed = 1.2f;
	private bool	canMove;
	public float 	waitTime = 1f;

	void Start() {
		canMove = false;
		Invoke ("CanMove", waitTime);
	}

	void Update() {
		if (canMove) {
			var pos = transform.position;
			pos.y += lavaSpeed * Time.deltaTime;
			transform.position = pos;
		}
	}

	private void CanMove() {
		canMove = true;
	}

	private void RollBackABit() {
		waitTime = 1f;
		canMove = false;
		Invoke ("CanMove", waitTime);
	}

	protected void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "BillRizer") {
			RollBackABit();
		}

		OnTriggerExit2D (other);
	}
}
