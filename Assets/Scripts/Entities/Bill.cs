using UnityEngine;
using System.Collections;

public class Bill : ContraEntity {
	
	public float 		leftBoundary = 1.2f;
	public bool 		isFallingThrough;
	public bool 		isCrouched;
	public bool 		inWater;
	public bool 		onFloor;
	public bool         onBridge;
	public float 		xSpeed = 0.05f;
	public float 		jumpVal = 10.5f;
	public Vector2		vel = Vector2.zero;
	public GameObject 	spawner;
	public float		gravityVal = -18f;
	private bool		invincibleFlag = false;
	private int			invincibleSeconds = 2;	
	public Gun 			gun;

	public	bool		invincibleMode = false;

	// Use this for initialization
	void Start () {
		this.gun = new BasicGun(this);
		controller = new BillController (this);
		leftOrRight = 1;
		health = 30;
		if (invincibleMode) health = 1000;
		Respawn ();
	}
	
	// Update is called once per frame
	void Update () {
		controller.Run ();
	}

	// Update is called once per frame
	void FixedUpdate () {	
		if (!onFloor && !inWater && !onBridge ) {
			// Apply gravity and acc to vel
			vel += new Vector2(0,gravityVal) * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2) transform.position + vel * Time.fixedDeltaTime;

		} else {
			vel = Vector2.zero;
		}
		
	}

	public override void MoveLeft() {
		if (!isFallingThrough && !isCrouched) {
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
		if (!isFallingThrough && !isCrouched) {
			Vector2 pos = transform.position;
			pos.x += xSpeed;
			transform.position = pos;
		}
	}

	public override void Jump() {
		if (!inWater && (onFloor || onBridge)) {
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
		onBridge = false;
		Vector2 jumpForce = new Vector2(0f, jumpVal);
		vel += (Vector2)jumpForce;
		transform.position =  (Vector2)transform.position +vel*Time.deltaTime;
	}

	public override void Crouch() {
		if (!isCrouched && !isFallingThrough && (onFloor || inWater)) {
			ScaleDown();
			isCrouched = true;
		}
	}

	public override void Uncrouch() {

		if (isCrouched) {
			ScaleUp();
			isCrouched = false;
		}
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

	public override void FallThrough() {
		isFallingThrough = true;
		onFloor = false;
		onBridge = false;
		Uncrouch();
	}

	private bool canFallThrough() {
		Vector2 pointA = (Vector2) renderer.bounds.min;

		Vector2 pointB = (Vector2) renderer.bounds.max;
		var vertExtent = Camera.main.camera.orthographicSize;
		pointB.y = -vertExtent;

		Collider2D[] others = Physics2D.OverlapAreaAll(pointA, pointB);

		bool haveSeenMyFloor = false;

		foreach (var other in others) {
			if (other.tag == "Floor" && other.enabled) {
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
	
	public override void Shoot() {
		gun.Shoot ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Floor") {
				if ((transform.position.y) < other.bounds.max.y) {
					if (!inWater) return;
			}
			onFloor = true;
			isFallingThrough = false;
			if (inWater) {
				ScaleUp();
				inWater = false;
			}
		
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y / 2; 

			transform.position = pos;

		} else if (other.tag == "Water") {
			inWater = true;
			onFloor = false;
			isFallingThrough = false;
			isCrouched = false;

			ScaleDown();

			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y / 2; 	
			transform.position = pos;

		} else if (other.tag == "Enemy") {
			this.Damage ();
		} else if (other.tag == "Bridge" && transform.position.y + transform.localScale.y/2 >= other.bounds.max.y) {
			this.onBridge = true;
		}
	}


	void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Floor") {
			onFloor = false;
		}
		else if (other.tag == "Bridge") {
			onBridge = false;
		}
		else if (other.tag == "Water") {
			if (inWater) ScaleUp();
			inWater = false;
		}
	}

	private void Respawn() {
		vel = Vector2.zero;
		onFloor = false;

		if (inWater) {
			ScaleUp();
			inWater = false;
		}

		transform.position = spawner.transform.position;
		leftOrRight = 1;
		// invincibleFlag = true;
		invincibleFlag = false;
		Invoke("SetVincible", invincibleSeconds);
		gun = new BasicGun (this);
	}

	private void SetVincible() {
		invincibleFlag = false;
	}


	public override void Damage(float damageTaken = 0) {

		bool isUnderWater = (inWater && isCrouched);

		if (invincibleFlag || isUnderWater || invincibleMode) {
			return;
		}

		//Debug.Log("Dead!!");
		// Do death animation
		health--;
		if (health > 0) {
			Respawn ();
		}
		else {
			Debug.Log("Game Over");
			Destroy	(gameObject);
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public void PowerUp(string powerUpType){
		if (powerUpType == "SGun") {
			this.gun = new SGun (this);
		} else if (powerUpType == "MGun") {
			this.gun = new MGun(this);
		} else if (powerUpType == "LGun") {
			this.gun = new MGun(this);
		} else if (powerUpType == "FGun") {
			this.gun = new MGun(this);
		}
	}
}
