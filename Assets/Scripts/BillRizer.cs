using UnityEngine;
using System.Collections;

public class BillRizer : MonoBehaviour {

	public float        leftEdge = -10f;
	public float        rightEdge = 10f;
	public float        topEdge = 10f;
	public float        bottomEdge = -10f;
	
	public float 		leftBoundary = 1.2f;
	public Vector2		gravity = new Vector2(0f,-9f);
	public Vector2		vel = Vector2.zero;
	public float 		xSpeed = 0.09f;
	public float 		jumpVal = 8.5f;

	public bool 		onFloor = false;
	public bool			inWater = false;
	public bool 		crouched = false;
	public bool 		fallingThrough = false;
	//private Animator	anim;

	public GameObject 	spawner;

	void Start () {
		//anim = GetComponent<Animator>();
	}


	void Update(){

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		float j = Input.GetAxisRaw ("Jump");
		float f = Input.GetAxisRaw ("Fire");

		if (h > 0 && !fallingThrough) {
			Vector2 pos = transform.position;
			pos.x += xSpeed;
			transform.position = pos;

		} else if (h < 0 && !fallingThrough) {
			Vector2 pos = transform.position;
			pos.x -= xSpeed;
			
			var vertExtent = Camera.main.camera.orthographicSize;   
			var horzExtent = vertExtent * Screen.width / Screen.height;
			
			if (pos.x >= (Camera.main.transform.position.x - horzExtent + leftBoundary)) {
				transform.position = pos;
			}
		}

		if (v < 0) {
				crouched = true;
		} else {
			crouched = false;
		}

		if (Input.GetKeyDown(KeyCode.X)) {
			
			if(inWater) return;
			
			if (onFloor) {
				onFloor = false; //you're going to leave the floor either way
				if (crouched) { // fall-through floor
					fallingThrough = true;
				}
				else { // jump off of floor
					Vector2 jumpForce = new Vector2(0f,jumpVal);
					vel += (Vector2)jumpForce;
					transform.position =  (Vector2)transform.position +vel*Time.deltaTime;
				}
			}
		}

		if (f > 0) { // fire a bullet horizontally for now

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (!onFloor && !inWater ) {
			// Apply gravity and acc to vel
				vel += (Vector2)Physics.gravity * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		} else {
			vel = Vector2.zero;
		}

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("BillRizer ! OnTriggerEnter2D!");
		if (other.tag == "Floor") {
			Debug.Log("Collided with a floor!");
			if( (transform.position.y) < other.bounds.max.y){
				Debug.Log("Feet not on floor!");
				if(!inWater)
					return;
			}
			onFloor = true;
			fallingThrough = false;
			//TODO:position BillRizer at the top of the floor collider box .
			//TODO: make sure BillRizer cannot go through the colliders (eg: If he's going at a high speed he'll fall straight through the collider)
			
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y/2; 
			
			transform.position = pos;
		}

		else if (other.tag == "Water") {
			Debug.Log("InWater!");
			inWater = true;
			onFloor = false;
			fallingThrough = false;
			crouched = false;
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y/2; 
			
			transform.position = pos;
		}

		else if (other.tag == "Bottom") {
			Debug.Log("Dead!!");
			// Do death animation
			vel = Vector2.zero;
			transform.position = spawner.transform.position;
		}
	}


	void OnTriggerStay2D (Collider2D other){
		//Debug.Log ("BillRizer ! OnTriggerStay2D!");
	}

	void OnTriggerExit2D (Collider2D other){
		Debug.Log ("BillRizer ! OnTriggerExit2D!");
		if (other.tag == "Floor") {
			onFloor = false;
		}

		if (other.tag == "Water") {
			inWater = false;
		}
	}
}
