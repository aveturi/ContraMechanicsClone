using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour{

	public bool 		onFloor;
	public Vector2		vel = Vector2.zero;
	public float		gravityVal = -18f;
	public float		newFiringRate = 0.7f;
	public string gunType = null;
	private float		jumpVal = 15f;
	private float		xSpeed = 2f;

	// Use this for initialization
	void Start () {
		onFloor = false;
		this.PerformJump ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!onFloor) {
			// Apply gravity and acc to vel
			vel += new Vector2(0,gravityVal) * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2) transform.position + vel * Time.fixedDeltaTime;
			
		} else {
			vel = Vector2.zero;
		}
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "BillRizer") {
			Bill bill = other.gameObject.GetComponent<Bill>();
			if(this.gunType != null){
				bill.PowerUp(gunType);
			} else {
				// The only other power-up in level 1 is the Rate powerup. Hardcoded for now.
				bill.gun.timeBetweenSteps = newFiringRate * bill.gun.timeBetweenSteps;
			}

			Destroy(this.gameObject);
		} else if (other.tag == "Floor") {

			if( (transform.position.y) < other.bounds.max.y){
				return;
			}
			onFloor = true;
			
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y / 2; 
			
			transform.position = pos;
		}
	}

	private void PerformJump() {
		onFloor = false;
		Vector2 jumpForce = new Vector2(xSpeed, jumpVal);
		vel += (Vector2)jumpForce;
		transform.position =  (Vector2)transform.position +vel*Time.deltaTime;
	}
}
