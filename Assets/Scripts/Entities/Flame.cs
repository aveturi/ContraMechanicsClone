using UnityEngine;
using System.Collections;

public class Flame : ContraEntity {

	public float flameFreq = 2;
	public float flameAmplitude = 3;
	float startTime;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (this.renderer.enabled) {
			var scale = this.transform.localScale;
			scale.y = Mathf.Sin ( (Time.time - startTime) * flameFreq) * flameAmplitude;
			this.transform.localScale = scale;
		} else {
			this.transform.localScale = new Vector3(this.transform.localScale.x,0,this.transform.localScale.z);
		}
	}

	public void StartFlame(){
		this.startTime = Time.time;
		this.renderer.enabled = true;
	}

	public void StopFlame(){
		this.renderer.enabled = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "BillRizer") {
			GameObject billObject = other.gameObject;
			Bill bill = billObject.GetComponent<Bill>();
			bill.Damage();
		}
	}


}
