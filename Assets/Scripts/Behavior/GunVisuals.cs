using UnityEngine;
using System.Collections;

public class GunVisuals : MonoBehaviour {

	public GameObject left;
	public GameObject right;
	public GameObject topLeft;
	public GameObject topRight;
	public GameObject bottomLeft;
	public GameObject bottomRight;
	public GameObject up;
	public GameObject down;

	private ContraEntity entity;
	// Use this for initialization
	void Start () {
		RenderersOff ();
		this.entity = this.transform.parent.gameObject.GetComponent<ContraEntity> ();
	}

	void Update(){
		this.transform.position = entity.transform.position;
		UpdateVisual ();
	}

	public void UpdateVisual(){
		Vector2 dir = entity.dir;
		RenderersOff ();

		Debug.Log ("UpdateGunVis " + entity.tag + " " + entity.dir);
		if (dir.x == -1 && dir.y == 0) { //L
				left.renderer.enabled = true;
		} else if (dir.x == 1 && dir.y == 0) { //R
				right.renderer.enabled = true;
		} else if (dir.x == -1 && dir.y == 1) { //TL
				topLeft.renderer.enabled = true;
		} else if (dir.x == 1 && dir.y == 1) { // TR
				topRight.renderer.enabled = true;
		} else if (dir.x == -1 && dir.y == -1) { // BL
				bottomLeft.renderer.enabled = true;
		} else if (dir.x == 1 && dir.y == -1) { //BR
				bottomRight.renderer.enabled = true;
		} else if (dir.x == 0 && dir.y == 1) {//UP
				up.renderer.enabled = true;
		} else if (dir.x == 0 && dir.y == -1) {//DOWN
				down.renderer.enabled = true;
		}
	}

	private void RenderersOff(){
		left.renderer.enabled = false;
		right.renderer.enabled = false;
		topLeft.renderer.enabled = false;
		topRight.renderer.enabled = false;
		bottomLeft.renderer.enabled = false;
		bottomRight.renderer.enabled = false;
		up.renderer.enabled = false;
		down.renderer.enabled = false;
	}
}
