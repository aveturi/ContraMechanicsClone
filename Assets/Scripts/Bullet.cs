using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Vector2 velocity;
	private float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}

	void SetVelocity(Vector2 velocity){
		this.velocity = velocity;
	}

	void FixedUpdate(){
	}

	// Update is called once per frame
	void Update () {
	
	}
}
