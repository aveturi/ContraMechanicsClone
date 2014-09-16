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
	public float 		jumpVal = 7f;


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
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Vector2 pos = transform.position;
		//pos.x += Input.GetAxis ("Horizontal") * Time.deltaTime * 3;
		//pos.y += Input.GetAxis ("Vertical");
		//transform.position = pos;

		bool apply_gravity = false;
		if (!onFloor && !inWater ) {
			// Apply gravity and acc to vel
			vel += (Vector2)Physics.gravity * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		} else {
			vel = Vector2.zero;
		}

		// X == A
		if (Input.GetKeyDown (KeyCode.X)) {
			//Debug.Log ("X!");
			if(inWater) return;

			if (onFloor) {
				if (crouched) {
					//Debug.Log("Fall through");
					onFloor = false;
					fallingThrough = true;
				}
				else {//JUMP
					Vector2 jumpForce = new Vector2 (0f, jumpVal);
					vel += (Vector2)jumpForce;
					transform.position = (Vector2)transform.position + vel * Time.deltaTime;
				}
			}
		}

		// Z == B
		if (Input.GetKeyDown (KeyCode.Z)) {
			//Debug.Log("Z!");
			
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Debug.Log("DOWN!");
			crouched = true;
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			Debug.Log("No longer crouched");
			crouched = false;

		}

		if (Input.GetKey(KeyCode.LeftArrow) && !fallingThrough){
			//Debug.Log("LEFT!");
			Vector2 pos = transform.position;
			pos.x -= xSpeed;
			
			var vertExtent = Camera.main.camera.orthographicSize;   
			var horzExtent = vertExtent * Screen.width / Screen.height;
				
			if (pos.x >= (Camera.main.transform.position.x - horzExtent + leftBoundary)) {
				transform.position = pos;
			}
		}

		if (Input.GetKey(KeyCode.RightArrow) && !fallingThrough){
			//Debug.Log("RIGHT!");
			Vector2 pos = transform.position;
			pos.x += xSpeed;
			transform.position = pos;		
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
