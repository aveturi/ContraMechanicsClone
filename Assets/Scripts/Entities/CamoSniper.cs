using UnityEngine;
using System.Collections;

public class CamoSniper : ContraEntity {

	public bool 	isCrouched;

	public float 	xRange = 10f;
	public float 	yRange = 1f;

	private float	lastStep;
	private int		bulletCount = 0;
	private float 	timeBetweenSteps = 5f;
	private	int		numMaxBullets = 1;

	private bool    activated = false;
	private float	screenWidth;

	private GameObject	bill;

	// Use this for initialization
	protected void Start () {
		controller = new CamoSniperController (this);
		bill = GameObject.FindGameObjectWithTag ("BillRizer");

		// set xRange so that CamoSniper only shoots once Bill can see it
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		xRange = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect)/2;
		screenWidth = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect);
	}
	
	// Update is called once per frame
	void Update () {
		controller.Run ();
	}


	public override void Shoot() {
		if (CanShoot ()) {
			PerformShoot();
		}
	}

	protected bool CanShoot(){

		var xDist = Mathf.Abs(Mathf.Abs(bill.transform.position.x) - Mathf.Abs(this.transform.position.x));
		var yDist = Mathf.Abs(Mathf.Abs(bill.transform.position.y) - Mathf.Abs(this.transform.position.y));


		if (xDist < xRange && yDist < yRange) {

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
					Debug.Log("LastStep "+ (Time.time - lastStep ));
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
		//Debug.Log ("CamoSniper Shoot!");
		GameObject bullet = Instantiate( bulletPrefab ) as GameObject;
		
		Vector3 pos = transform.position;
		pos.x += ((transform.localScale.x/2 + bulletDeltaSpace) * (leftOrRight));
		
		bullet.transform.position = pos;
		
		Bullet b = bullet.GetComponent<Bullet>();
		b.speed *= 0.5f;
		b.owner = this;
		b.SetVelocity(dir);
		bulletCount++;
	}


	public override void Damage(float damageTaken = 0) {
		//Debug.Log("Dead!!");

		//TODO: Do death animation

		Destroy (this.gameObject);
	}
	

	public override void Crouch() {
		isCrouched = true;
		//TODO: wait here for a short time lag
	}


	public override void Uncrouch() {
		isCrouched = false;
	}
}
