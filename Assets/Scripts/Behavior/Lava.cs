using UnityEngine;
using System.Collections;

public class Lava : Boundary {

	private float 	lavaSpeed = 1.3f;
	private float 	startingYDelta = 1.5f;
	private bool	canMove;
	public float 	waitTime = 1.5f;

	void Start() {
		ResetPosition ();
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

	private void ResetPosition () {
		Vector2 t_a = new Vector2 (0f, 0f); // will be the Bottom-Left
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		t_a = mainCamera.camera.ViewportToWorldPoint (t_a);

		Vector3 pointA = new Vector3(t_a.x, t_a.y);

		pointA.x += renderer.bounds.size.x/2;
		pointA.y -= renderer.bounds.size.y / 2 - startingYDelta;
		pointA.z = -0.3f;
		transform.position = pointA;
	}

	private void StopRising() {
		waitTime = 1f;
		canMove = false;

		GameObject spawner = GameObject.Find("BillSpawnerMarker");
		BillVerticalSpawner spawnerScript = (BillVerticalSpawner) spawner.GetComponent(typeof(BillVerticalSpawner));
		spawnerScript.PositionSpawner();

		Invoke ("CanMove", waitTime);
	}

	protected void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "BillRizer") {
			VerticalCameraTracking cameraScript = (VerticalCameraTracking) Camera.main.GetComponent(typeof(VerticalCameraTracking));

			ResetPosition ();
			StopRising ();
		}

		else if (other.tag == "Floor") {
		}

		OnTriggerExit2D (other);
	}
}
