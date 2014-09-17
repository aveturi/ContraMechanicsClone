using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Vector2 velocity = Vector2.zero;
	private float speed = 8f;
	// Use this for initialization
	void Start () {
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
		if (other.tag != "Floor" && other.tag != "Water") {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			entity.Damage(10);
			Destroy (this.gameObject);
		}
	}
}
