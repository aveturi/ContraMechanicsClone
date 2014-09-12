using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Started floor!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("OnTriggerEnter2D!");

		
	}

}
