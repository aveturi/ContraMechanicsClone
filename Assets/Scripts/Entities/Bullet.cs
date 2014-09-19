using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

	private 		Vector2 velocity = Vector2.zero;
	public 			float speed = 8f;
	private 		List<string> safeTags;
	protected 		int damageVal = 1;
	public			ContraEntity owner { set; get; }

	// Use this for initialization
	void Awake () {
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
		if (other != null && other.tag != null && !safeTags.Contains(other.tag) && other.tag != owner.tag) {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			entity.Damage(damageVal);
			Destroy (this.gameObject);
		}
	}
}
