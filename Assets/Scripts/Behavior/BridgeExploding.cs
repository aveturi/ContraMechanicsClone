using UnityEngine;
using System.Collections;

public class BridgeExploding : MonoBehaviour {

	bool timerStart = false;
	double startTime;
	public double delta = 0f;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "BillRizer") {

			//TODO: do destruction graphics
			if(!timerStart){
				timerStart = true;
				startTime = Time.time;
			}
		}
	}

	void Update(){
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
