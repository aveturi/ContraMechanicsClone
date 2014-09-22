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
	protected virtual void Start () {	
		controller = new CamoSniperController (this);
		bill = GameObject.FindGameObjectWithTag ("BillRizer");

		// set xRange so that CamoSniper only shoots once Bill can see it
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		xRange = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect)/2;
		screenWidth = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		controller.Run ();
	}


	public override void Shoot() {
		if (CanShoot ()) {
			PerformShoot();
		}
	}

	protected bool CanShoot(){

		var xDist = Mathf.Abs(bill.transform.position.x - this.transform.position.x);
		var yDist = Mathf.Abs(bill.transform.position.y - this.transform.position.y);


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
		// pos.x += ((transform.localScale.x/2 + bulletDeltaSpace) * (leftOrRight));
		pos.z = 0.1f;
		bullet.transform.position = pos;

		Bullet b = bullet.GetComponent<Bullet>();
		b.owner = this;
		b.ownerTag = this.tag;
		b.SetVelocity(dir);
		bulletCount++;
	}

	private void ScaleUp() {
		var t_y = renderer.bounds.min.y;
		
		Vector3 scale = transform.localScale;
		scale.y = scale.y * 2 ;
		transform.localScale = scale;
		
		var pos = transform.position;
		pos.y += (pos.y - t_y) ;
		transform.position = pos;
	}
	
	private void ScaleDown() {
		var t_y = renderer.bounds.min.y;
		
		Vector3 scale = transform.localScale;
		scale.y = scale.y / 2 ;
		transform.localScale = scale;
		
		var pos = transform.position;
		pos.y -= (pos.y - t_y) / 2;
		transform.position = pos;
	}

	public override void Damage(float damageTaken = 0) {
		//Debug.Log("Dead!!");

		//TODO: Do death animation

		Destroy (this.gameObject);
	}
	

	public override void Crouch() {
		if (!isCrouched) {
			ScaleDown ();		
		}
		isCrouched = true;
		//TODO: wait here for a short time lag
	}


	public override void Uncrouch() {
		if (isCrouched) {
			ScaleUp ();	
		}
		isCrouched = false;
	}
}
