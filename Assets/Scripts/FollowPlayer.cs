using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject		player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position;
		pos.z = -10.0F;
		transform.position = pos;
	}
}
