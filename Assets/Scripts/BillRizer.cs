using UnityEngine;
using System.Collections;

public class BillRizer : MonoBehaviour {

	public float        leftEdge = -10f;
	public float        rightEdge = 10f;
	public float        topEdge = 10f;
	public float        bottomEdge = -10f;

	public Vector2		gravity = new Vector2(0f,-9f); //TODO:tune this.
	public Vector2		vel = Vector2.zero;

	public float xSpeed = 0.09f;
	public float jumpVal = 7f;


	public bool onFloor = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		//Vector2 pos = transform.position;
		//pos.x += Input.GetAxis ("Horizontal") * Time.deltaTime * 3;
		//pos.y += Input.GetAxis ("Vertical");
		//transform.position = pos;

		if (!onFloor) {
			// Apply gravity and acc to vel
			vel += (Vector2)Physics.gravity * Time.fixedDeltaTime;

			// Apple vel to position
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		} else {
			vel = Vector2.zero;
		}

		// X == A
		if (Input.GetKeyDown (KeyCode.X)) {
			Debug.Log ("X!");
			if (onFloor) {
					//JUMP
					//Vector2 pos = transform.position;
					Vector2 jumpForce = new Vector2 (0f, jumpVal);
					vel += (Vector2)jumpForce;
					transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
					//transform.position = pos;

			}
		}

		// Z == B
		if (Input.GetKey (KeyCode.Z)) {
			Debug.Log("Z!");
			
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log("DOWN!");
			//FallThroughFloor()
		}

		if (Input.GetKey(KeyCode.LeftArrow)){
			Debug.Log("LEFT!");
			Vector2 pos = transform.position;
			pos.x -= xSpeed;
			transform.position = pos;
			
		}

		if (Input.GetKey(KeyCode.RightArrow)){
			Debug.Log("RIGHT!");
			Vector2 pos = transform.position;
			pos.x += xSpeed;
			transform.position = pos;

		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			Debug.Log("UP!");
		}




	}


	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("BillRizer ! OnTriggerEnter2D!");
		if (other.tag == "Floor") {

			onFloor = true;
			//TODO:position BillRizer at the top of the floor collider box .
			//TODO: make sure BillRizer cannot go through the colliders (eg: If he's going at a high speed he'll fall straight through the collider)

			Collider2D coll = GetComponent<Collider2D>();
			Vector2 pos = transform.position;
			pos.y = other.bounds.max.y + transform.localScale.y/2; 

			transform.position = pos;
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
	}
}
