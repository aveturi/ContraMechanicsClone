using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

	public bool 		onFloor = false;
	public Vector2		vel = Vector2.zero;
	public float 		xSpeed = -1f;
	public float 		jumpVal = 7.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!onFloor) {
			// Apply gravity and acc to vel
			vel += (Vector2)Physics.gravity * Time.fixedDeltaTime;
			
			// Apple vel to position
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		} else {
			vel = new Vector2(xSpeed,0f);
	
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Floor") {

				if( (transform.position.y) < other.bounds.max.y){
						return;
				}
				onFloor = true;

				Vector2 pos = transform.position;
				pos.y = other.bounds.max.y + transform.localScale.y / 2; 
	
				transform.position = pos;
		} else if (other.tag == "BillRizer") {
			// make billrizer die.
		}

	}

	void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Floor") {
			Jump();
		} else if (other.tag == "Water" || other.tag == "Bullet") {
			this.Damage();
		}
	}

	void Damage(){
		Debug.Log ("AI die!");
		Destroy (this.gameObject);
	}

	void Jump(){
		Debug.Log("AI jump!!");
		if (onFloor) {
			onFloor = false; //you're going to leave the floor either way
			Vector2 jumpForce = new Vector2(0f,jumpVal);
			vel += (Vector2)jumpForce;
			transform.position =  (Vector2)transform.position +vel*Time.deltaTime;
		}
	}
}
