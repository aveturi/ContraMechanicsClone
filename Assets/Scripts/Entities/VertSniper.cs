using UnityEngine;
using System.Collections;

public class VertSniper : Sniper {

	public override bool onCamera(){
		// set xRange so that Sniper only shoots once Bill can see it
		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		var leftPoint = mainCamera.camera.ViewportToWorldPoint (new Vector2 (0f,0f));
		var rightPoint = mainCamera.camera.ViewportToWorldPoint (new Vector2 (1f,1f));
		return (this.transform.position.x > leftPoint.x && this.transform.position.x < rightPoint.x &&
		        this.transform.position.y > leftPoint.y && this.transform.position.y < rightPoint.y);
	}
}
