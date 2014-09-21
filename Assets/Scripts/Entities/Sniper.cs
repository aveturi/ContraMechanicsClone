using UnityEngine;
using System.Collections;

public class Sniper : ContraEntity {
	protected float 	xRange;
	protected float 	screenWidth;
	protected bool    	activated = false;
	public	bool 		isWaterSniper = false;
	protected float		lastStep;
	protected int		bulletCount = 0;
	protected float 	timeBetweenSteps = 3f;
	protected int		numMaxBullets = 3;
	
	protected virtual void Start () {
		controller = new SniperController (this);

		// set xRange so that Sniper only shoots once Bill can see it
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		screenWidth = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect);
		xRange = screenWidth/2;
		
	}

	protected float t_lastStep = 0;
	protected float t_timeBetweenSteps = 0.5f;

	protected virtual void Update(){
		if (t_lastStep == 0) {
			t_lastStep = Time.time;
		}
		else if (Time.time - t_lastStep > t_timeBetweenSteps) {
			controller.Run ();
			t_lastStep = Time.time;
		}
	}

	public override void Shoot() {
		if (CanShoot ()) {
			// Debug.Log ("Sniper canshoot!");
			PerformShoot ();
		} else {
			// Debug.Log("Sniper can't shoot!");
		}
	}


	protected virtual bool CanShoot(){
		var bill = GameObject.FindGameObjectWithTag ("BillRizer");
		
		var xDist = Mathf.Abs(Mathf.Abs(bill.transform.position.x) - Mathf.Abs(this.transform.position.x));

		if (xDist < xRange ) {
			
			if(!activated){
				activated = true;
				xRange = screenWidth;
			}
			
			if (bulletCount < numMaxBullets) {
				return true;
			} else {
				if (lastStep == 0) {
					lastStep = Time.time;
				} else if (Time.time - lastStep > timeBetweenSteps) {
					lastStep = Time.time;
					bulletCount = 0;
					return true;
				}
				
				return false;
			}
		} else {
			return false;
		}
	}
	
	protected virtual void PerformShoot() {
		//Debug.Log ("Sniper shoot!");
		GameObject bullet = Instantiate( bulletPrefab ) as GameObject;
		
		Vector3 pos = transform.position;
		pos.x += ((transform.localScale.x/2 + bulletDeltaSpace) * (leftOrRight));
		// pos.y += ((transform.localScale.y/2 + bulletDeltaSpace)  * (upOrDown)); 
		// Not needed since bullets are always instantiated from the side, right?

		bullet.transform.position = pos;
		
		Bullet b = bullet.GetComponent<Bullet>();
		b.speed *= 0.5f;
		b.owner = this;
		b.SetVelocity(dir);
		bulletCount++;
	}
	
	public override void Damage(float damageTaken = 0) {
		Destroy (this.gameObject);
	}
}
