using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

	public GameObject		player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = player.transform.position.x;
		if (pos.x >= transform.position.x) {
			transform.position = pos;
		}
	}
}
