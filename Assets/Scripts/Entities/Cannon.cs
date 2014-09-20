using UnityEngine;
using System.Collections;

public class Cannon : CamoSniper {

	void Start () {
		base.Start ();
		controller = new CannonController (this);
		health = 6;
	}
	
	public override void Damage(float damageTaken) {
		health -= damageTaken;
		if (health == 0) {
			Debug.Log("Cannon destroyed");
			Destroy (gameObject);
		}
	}
}
