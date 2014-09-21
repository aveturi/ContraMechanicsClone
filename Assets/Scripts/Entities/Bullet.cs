using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

	private 		Vector2 velocity = Vector2.zero;
	public 			float speed = 8f;
	private 		List<string> safeTags;
	protected 		float damageVal = 1f;
	public			ContraEntity owner { set; get; }
	public			string ownerTag;
	public			float screenWidth;
	// Use this for initialization
	void Awake () {
		safeTags = new List<string>()
		{
			"Floor",
			"Bridge",
			"Boundary",
			"Water",
			"Bullet",
			"PowerUp"
		};
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		screenWidth = (mainCamera.camera.orthographicSize * 2f * mainCamera.camera.aspect);
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
		// if the x distance from the owner is > 1 screenWidth, kill bullet

		if (this.owner == null)
						return;
		var distanceFromOwner = Mathf.Abs(this.owner.transform.position.x - this.transform.position.x);

		if (distanceFromOwner > this.screenWidth) {
			if(this.gameObject != null){
				Destroy(this.gameObject);
			}
		}

		ownerTag = this.owner.tag;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other != null && other.tag != null && !safeTags.Contains(other.tag) && other.tag != this.ownerTag) {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			entity.Damage(damageVal);
			Destroy (this.gameObject);
		}
	}

	public void SetDamage(float d){
		this.damageVal = d;
	}

	public void SetSpeed(float s){
		speed = s;
	}
}
