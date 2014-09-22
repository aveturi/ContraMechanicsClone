using UnityEngine;
using System.Collections;

public class Cannon : Sniper {

	public float		cannonRange = 8f;

	protected override void Start () {
		base.Start ();
		controller = new CannonController (this);
		health = 6;
		numMaxBullets = 1;
		timeBetweenSteps = 0f;
		t_timeBetweenSteps = 3f;
		renderer.enabled = false;
	}

	protected virtual void Update() {
		base.Update ();

		var bill = GameObject.FindGameObjectWithTag ("BillRizer");
		bool inRange = false;

		if (bill != null) {
			inRange = (bill.transform.position.x + cannonRange >= transform.position.x);
		}

		if (inRange) {
			renderer.enabled = true;	
		}

	}
	
	public override void Damage(float damageTaken) {
		health -= damageTaken;
		if (health <= 0) {
			Debug.Log("Cannon destroyed");
			Destroy (gameObject);
		}
	}
}
