using UnityEngine;
using System.Collections;

public class VerticalCameraTracking : MonoBehaviour {
	
	private GameObject	player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("BillRizer");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			return;
		}

		Vector3 pos = transform.position;
		
		pos.y = player.transform.position.y;
		if (pos.y >= transform.position.y) {
			transform.position = pos;
		}
	}

	public void Center() {
		if (player == null) {
			return;
		}
		Vector3 pos = transform.position;
		pos.y = player.transform.position.y;
		transform.position = pos;
	}
	
}
