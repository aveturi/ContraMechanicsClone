using UnityEngine;
using System.Collections;

public class BridgeExploding : MonoBehaviour {
	
	Bill bill;

	void Start() {
		GameObject billObject = GameObject.FindGameObjectWithTag("BillRizer");
		bill = billObject.GetComponent<Bill>();
	}

	void Update() {
		if (shouldExplode ()) {
			StartCoroutine(Explode());
		}
	}

	bool shouldExplode() {
		float min_x = this.renderer.bounds.min.x;
		return min_x < bill.renderer.bounds.max.x;
	}

	IEnumerator Explode() {
		yield return new WaitForSeconds(0.6f);
		ExplodeHelper ();
	}

	void ExplodeHelper() {
		Destroy(this.gameObject);
	}

	
}
