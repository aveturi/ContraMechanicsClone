using UnityEngine;
using System.Collections;

public class VerticalCameraTracking : MonoBehaviour {
	
	private GameObject	player;
	public Color color1;
	public Color color2;
	private float duration = 5f;
	private float deltaTime = 0.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("BillRizer");
		deltaTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		deltaTime += Time.deltaTime;
		if(deltaTime < duration)
		{
			camera.backgroundColor = Color.Lerp(color1, color2, deltaTime/duration);

		}
		else { 
			deltaTime = 0.0f; 
			
			var _t = color1;
			color1 = color2;
			color2 = _t;
		}

		if (player == null) {
			return;
		}

		Vector3 pos = transform.position;
		
		pos.y = player.transform.position.y;
		if (pos.y >= transform.position.y) {
			transform.position = pos;
		}
		var bottomLeft = this.camera.ViewportToWorldPoint (new Vector2(0, 0));
		if (player.renderer.bounds.min.y <= bottomLeft.y) {
			Bill bill = player.GetComponent<Bill>();
			bill.Damage(-1);
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
