using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	GameObject lava;
	public bool isWaterFloor = false; 
	// Use this for initialization
	void Start () {
		if (Application.loadedLevel == 1) {
			lava = GameObject.FindGameObjectWithTag ("Lava");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.loadedLevel == 1) {
			var lavaTop = lava.renderer.bounds.max.y;
			collider2D.enabled = (lavaTop < collider2D.bounds.min.y);
		}
	}

	void onTriggerEnter2D(Collider2D other){
		Debug.Log ("BILL ON FLOOR");
	}
}
