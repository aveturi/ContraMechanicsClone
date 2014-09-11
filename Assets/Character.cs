using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public float 		speed = 6f;
	public float        leftEdge = -10f;
	public float        rightEdge = 10f;
	public float        topEdge = 10f;
	public float        bottomEdge = -10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;

		if (Input.GetKey ("up")) {
			pos.y += speed * Time.deltaTime;
		}

		if (Input.GetKey ("down")) {
			pos.y -= speed * Time.deltaTime;
		}

		if (Input.GetKey ("right")) {
			pos.x += speed * Time.deltaTime;
		}

		if (Input.GetKey ("left")) {
			pos.x -= speed * Time.deltaTime;
		}

		if ( pos.x > leftEdge && pos.x < rightEdge && pos.y > bottomEdge && pos.y < topEdge) {
			transform.position = pos;
		}

	}
}
