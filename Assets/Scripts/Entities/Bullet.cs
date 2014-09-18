using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

	private 		Vector2 velocity = Vector2.zero;
	public 			float speed = 8f;
	private 		List<string> safeTags;
	protected 		int damageVal = 10;

	// Use this for initialization
	void Start () {
		safeTags = new List<string>()
		{
			"Floor",
			"Bridge",
			"Boundary",
			"Water",
			"Bullet"
		};
	}

	public void SetVelocity(Vector2 velocity){
		velocity.Normalize ();
		this.velocity = velocity * speed;
	}

	void FixedUpdate(){
		transform.position = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (!safeTags.Contains(other.tag)) {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			entity.Damage(damageVal);
			Destroy (this.gameObject);
		}
	}
}
