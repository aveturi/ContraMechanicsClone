using UnityEngine;
using System.Collections;

public class Bill : ContraEntity {
	
	public float 		leftBoundary = 1.2f;
	public bool 		isFallingThrough;
	public bool 		isCrouched;
	public bool 		inWater;
	public bool 		onFloor;
	public float 		xSpeed = 0.05f;
	public float 		jumpVal = 10.5f;
	public Vector2		vel = Vector2.zero;
	public GameObject 	spawner;
	public float		gravityVal = -18f;

	// Use this for initialization
	void Start () {
		controller = new BillController (this);
		leftOrRight = 1;
		health = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		controller.Run ();
	}

	// Update is called once per frame
	void FixedUpdate () {	
		if (!onFloor && !inWater ) {
			// Apply gravity and acc to vel
			vel += new Vector2(0,gravityVal) * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2) transform.position + vel * Time.fixedDeltaTime;

		} else {
			vel = Vector2.zero;
		}
		
	}

	public override void MoveLeft() {
		if (!isFallingThrough) {
			Vector2 pos = transform.position;
			pos.x -= xSpeed;
			
			var vertExtent = Camera.main.camera.orthographicSize;
			var horzExtent = vertExtent * Screen.width / Screen.height;
			
			if (pos.x >= (Camera.main.transform.position.x - horzExtent + leftBoundary)) {
				transform.position = pos;
			}
		}
	}

	public override void MoveRight() {
		if (!isFallingThrough) {
			Vector2 pos = transform.position;
			pos.x += xSpeed;
			transform.position = pos;
		}
	}

	public override void Jump() {
		if (!inWater && onFloor) {
			if (isCrouched) {
				if (canFallThrough()) {
					FallThrough();
				}
			}
			else {
				PerformJump();
			}
		}
	}

	private void PerformJump() {
		onFloor = false;
		Vector2 jumpForce = new Vector2(0f, jumpVal);
		vel += (Vector2)jumpForce;
		transform.position =  (Vector2)transform.position +vel*Time.deltaTime;
	}

	public override void Crouch() {
		isCrouched = true;
	}

	public override void Uncrouch() {
		isCrouched = false;
	}

	public override void FallThrough() {
		isFallingThrough = true;
		onFloor = false;
	}

	private bool canFallThrough() {
		Vector2 pointA = (Vector2) renderer.bounds.min;

		Vector2 pointB = (Vector2) renderer.bounds.max;
		var vertExtent = Camera.main.camera.orthographicSize;
		pointB.y = -vertExtent;

		Collider2D[] others = Physics2D.OverlapAreaAll(pointA, pointB);

		bool haveSeenMyFloor = false;

		foreach (var other in others) {
			if (other.tag == "Floor") {
				if (haveSeenMyFloor) {
					return true;
				}
				else {
					haveSeenMyFloor = true;
				}
			}
			else if (other.tag == "Water") {
				return true;
			}
		}

		return false;
	}

	private float	lastStep;
	private int		bulletCount = 0;
	private float 	timeBetweenSteps = 2f;
	private	int		numMaxBullets = 4;

	public override void Shoot() {
		if (canShoot()) {
			PerformShoot();
			bulletCount++;
		}
	}

	private bool canShoot() {
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

	private void PerformShoot() {
		GameObject bullet = Instantiate( bulletPrefab ) as GameObject;
		
		Vector3 pos = transform.position;
		pos.x += ((transform.localScale.x/2 + bulletDeltaSpace) * (leftOrRight));
	
		bullet.transform.position = pos;
		
		Bullet b = bullet.GetComponent<Bullet>();
		b.owner = this;
		b.SetVelocity(dir);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Floor") {
			if ((transform.position.y) < other.bounds.max.y) {

					if (!inWater)
							return;
			}
			onFloor = true;
			isFallingThrough = false;
			//TODO:position BillRizer at the top of the floor collider box .
			//TODO: make sure BillRizer cannot go through the colliders (eg: If he's going at a high speed he'll fall straight through the collider)

			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y / 2; 

			transform.position = pos;
		} else if (other.tag == "Water") {
			//Debug.Log("InWater!");
			inWater = true;
			onFloor = false;
			isFallingThrough = false;
			isCrouched = false;
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y / 2; 	
			transform.position = pos;
		} else if (other.tag == "Enemy") {
			this.Damage();
		}
	}
	
	void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Floor") {
			onFloor = false;
		}
		
		if (other.tag == "Water") {
			inWater = false;
		}
	}

	private void Respawn() {
		vel = Vector2.zero;
		transform.position = spawner.transform.position;
		bulletCount = 0;
		leftOrRight = 1;
	}

	public override void Damage(int damageTaken = 0) {
		Debug.Log("Dead!!");
		// Do death animation
		health--;
		if (health > 0) {
			Respawn ();
		}
		else {
			Debug.Log("Game Over");
			Destroy	(gameObject);
		}
	}
}
