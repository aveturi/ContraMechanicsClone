using UnityEngine;
using System.Collections;

public class Cannon : Sniper {

	protected override void Start () {
		base.Start ();
		controller = new CannonController (this);
		health = 6;
		numMaxBullets = 1;
		timeBetweenSteps = 0f;
		t_timeBetweenSteps = 4f;
	}
	
	public override void Damage(float damageTaken) {
		health -= damageTaken;
		if (health == 0) {
			Debug.Log("Cannon destroyed");
			Destroy (gameObject);
		}
	}
}
