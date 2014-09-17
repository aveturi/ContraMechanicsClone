using UnityEngine;
using System.Collections;

public class CamoSniper : ContraEntity {

	public bool isCrouched;
	public bool facingLeft;
	public bool inRange;
	public float range = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var bill = GameObject.FindGameObjectWithTag ("BillRizer");
		if (bill == null) {
			return;
		} else {
			facingLeft = (bill.transform.position.x < this.transform.position.x);
			var xDist = Mathf.Abs(Mathf.Abs(bill.transform.position.x) - Mathf.Abs(this.transform.position.x));
			inRange = (xDist < range);
		}
	}

	void FixedUpdate(){
		var bill = GameObject.FindGameObjectWithTag ("BillRizer");
		if (bill == null) {
			return;
		}


		if (canShoot()) {
			PerformShoot ();
		}
	}

	public GameObject   bulletPrefab;
	private float 		bulletDeltaSpace = 0.3f;

	private void PerformShoot() {
		GameObject bullet = Instantiate( bulletPrefab ) as GameObject;
		
		Vector3 pos = transform.position;
		pos.x += transform.localScale.x/2 + bulletDeltaSpace;
		bullet.transform.position = pos;
		
		Bullet b = bullet.GetComponent<Bullet>();

		Vector2 bulletVelocity = new Vector2 (1f, 0f);

		if (facingLeft) {
			bulletVelocity.x*=-1;
		}
		b.SetVelocity(bulletVelocity);

		bulletCount++;
	}


	public override void Damage(int damageTaken = 0) {
		Debug.Log("Dead!!");
		/*if (isCrouched) {
			return;
		}*/

		//TODO: Do death animation

		Destroy (this.gameObject);
	}

	private float	lastStep;
	private int		bulletCount = 0;
	private float 	timeBetweenSteps = 2f;
	private	int		numMaxBullets = 4;

	private bool canShoot() {

		if (!inRange) {
			return false;
		}

		if (bulletCount < numMaxBullets) {
			return true;
		}
		else {
			if (lastStep == 0) {
				lastStep = Time.time;
			}
			
			else if (Time.time - lastStep > timeBetweenSteps) {
				lastStep = Time.time;
				bulletCount = 0;
				return true;
			}
			
			return false;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Bullet") {
			this.Damage();
		}
	}
}
