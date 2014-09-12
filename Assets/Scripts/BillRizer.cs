using UnityEngine;
using System.Collections;

public class BillRizer : MonoBehaviour {

	public float        leftEdge = -10f;
	public float        rightEdge = 10f;
	public float        topEdge = 10f;
	public float        bottomEdge = -10f;

	public Vector2		gravity = new Vector2(0f,-0.5f); //TODO:tune this.
	public Vector2		vel = Vector2.zero;

	public bool onFloor = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 pos = transform.position;
		if (Input.GetKey ("up")) {
			pos.y += 0.3f ; //jump
		}

		if (Input.GetKey ("down")) {
			pos.y -= 0.05f ;
		}

		if (Input.GetKey ("right")) {
			pos.x += 0.03f ;
		}

		if (Input.GetKey ("left")) {
			pos.x -= 0.03f ;
		}

		if ( pos.x > leftEdge && pos.x < rightEdge && pos.y > bottomEdge && pos.y < topEdge) {
			transform.position = pos;
		}

	}

	void FixedUpdate(){
		if (!onFloor) {
			// Apply gravity and acc to vel
			vel += gravity * Time.fixedDeltaTime;
			//vel += acc * Time.fixedDeltaTime;

			// Apple vel to position
			transform.position = (Vector2)transform.position + vel * Time.fixedDeltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("BillRizer ! OnTriggerEnter2D!");
		if (other.tag == "Floor") {
			onFloor = true;
			//TODO:position BillRizer at the top of the floor collider box .
			//TODO: make sure BillRizer cannot go through the colliders (eg: If he's going at a high speed he'll fall straight through the collider)
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
