using UnityEngine;
using System.Collections;

public class StationaryPowerUpContainer : MonoBehaviour {

	public GameObject powerup; 
	public string gunType = "SGun";
	public bool open = false;

	public float 	timeBetweenSteps = 2f;
	public float 	lastStep = 0;

	// Use this for initialization
	void Start () {
		powerup = Resources.Load("PowerUp") as GameObject;
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Bullet" && open) {

			Bullet bullet = other.gameObject.GetComponent<Bullet>();
			if(bullet.owner.tag == "BillRizer"){

				powerup.transform.position = this.transform.position;
				PowerUp p = powerup.GetComponent<PowerUp>();
				p.gunType = this.gunType;

				Instantiate(powerup);
				Destroy(this.gameObject);
			}
		}
	}

	void FixedUpdate(){
		if (lastStep == 0) {
			lastStep = Time.time;
		}
		
		else if (Time.time - lastStep > timeBetweenSteps) {
			lastStep = Time.time;

			if(this.open == false) {
				//Debug.Log ("Open"); 
				this.open = true;
			} else if(this.open == true){
				//Debug.Log("Closed");
				this.open = false;
			}
		}
	}
}
