﻿using UnityEngine;
using System.Collections;

public class BridgeExploding : MonoBehaviour {

	bool timerStart = false;
	float startTime;
	public float delta = 1f;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "BillRizer") {

			//TODO: do destruction graphics
			//TODO: Make BillRizer stay on bridge
			if(!timerStart){
				timerStart = true;
				startTime = Time.time;
			}
		}
	}

	void FixedUpdate(){
		if (timerStart) {
			if(Time.time > (this.startTime+delta)){
				Debug.Log(this.gameObject.name +" has been destroyed");
				GameObject billObject = GameObject.FindGameObjectWithTag("BillRizer");
				Bill bill = billObject.GetComponent<Bill>();
				bill.onBridge = false;
				Destroy(this.gameObject);
			}
		}
	}
	
}
