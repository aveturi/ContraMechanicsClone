using UnityEngine;
using System.Collections;

public class Sniper : ContraEntity {
	private float 	xRange;
	private float 	screenWidth;
	private bool    activated = false;

	private float	lastStep;
	private int		bulletCount = 0;
	private float 	timeBetweenSteps = 3f;
	private	int		numMaxBullets = 3;
	
	void Start () {
		controller = new SniperController (this);
		
		// set xRange so that Sniper only shoots once Bill can see it
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		xRange = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect)/2;
		screenWidth = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect);
	}

	void Update(){
		controller.Run ();
	}

	public override void Shoot() {
		if (CanShoot ()) {
			// Debug.Log ("Sniper canshoot!");
			PerformShoot ();
		} else {
			// Debug.Log("Sniper can't shoot!");
		}
	}


	private bool CanShoot(){
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
	
	private void PerformShoot() {
		//Debug.Log ("Sniper shoot!");
		GameObject bullet = Instantiate( bulletPrefab ) as GameObject;
		
		Vector3 pos = transform.position;
		pos.x += ((transform.localScale.x/2 + bulletDeltaSpace) * (leftOrRight));
		// pos.y += ((transform.localScale.y/2 + bulletDeltaSpace)  * (upOrDown)); 
		// Not needed since bullets are always instantiated from the side, right?

		bullet.transform.position = pos;
		
		Bullet b = bullet.GetComponent<Bullet>();
		b.speed *= 0.5f;
		b.SetVelocity(dir);
		bulletCount++;
	}
	
	public override void Damage(int damageTaken = 0) {
		Destroy (this.gameObject);
	}
}
