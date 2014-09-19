using UnityEngine;
using System.Collections;

public class WallTurret : Sniper {

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 5;
	}

	public override void Damage(int damageTaken) {
		health -= damageTaken;
		if (health == 0) {
			Debug.Log("Wall Turret destroyed");
			Destroy (gameObject);
		}
	}
}
