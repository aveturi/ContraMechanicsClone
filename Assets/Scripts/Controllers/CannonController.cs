using UnityEngine;
using System.Collections;

public class CannonController : Controller {
	
	private GameObject 		bill = null;

	public CannonController (ContraEntity entity) : base(entity) 
	{
		bill = GameObject.FindGameObjectWithTag ("BillRizer");
	}
	
	public override void Run () {
			if (bill == null) {
				return;	
			}

		var mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		Vector2 screenPos = mainCamera.camera.WorldToViewportPoint(entity.transform.position);
		if(screenPos.x > 1.0 || screenPos.x <0.0)
			return;
			entity.leftOrRight = -1;
			entity.dir = new Vector2(entity.leftOrRight, 0);
			
			if (bill.transform.position.x < entity.transform.position.x) {
				entity.Shoot();
			}

	}
}
