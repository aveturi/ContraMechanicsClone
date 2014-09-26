using UnityEngine;
using System.Collections;

public class FlameBall : ContraEntity {

	private float timeBetweenSteps = 1.5f;
	private float lastStep = 0;
	private bool flameOn = false;
	void Start () {
		if(this.health == 0)
		this.health = 10;
	}
	


	void Update () {
	
	}

	void FixedUpdate(){
		if (CanShoot ()) {
			RenderFlames(flameOn);
			flameOn = ! flameOn;
		}
	}

	private bool CanShoot() {
		if (lastStep == 0) {
			lastStep = Time.time;
		}
		
		else if (Time.time - lastStep > timeBetweenSteps) {
			lastStep = Time.time;
			return true;
		}
		
		return false;
	}

	private void RenderFlames (bool renderFlag)
	{
		GameObject parent = transform.parent.gameObject;
		var Flames = parent.GetComponentsInChildren<Flame>();
		
		foreach (Flame flame in Flames) {
			if(renderFlag)
				flame.StartFlame();
			else
				flame.StopFlame();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("FlameBall collided with "+other.tag);
	}

	public override void Damage (float damageTaken)
	{
		this.health--;
		if (this.health == 0) {
			GameObject parent = transform.parent.gameObject;
			Destroy(parent);
		}
	}
}
