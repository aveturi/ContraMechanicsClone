using UnityEngine;
using System.Collections;

public class CannonController : SniperController {

	public CannonController (ContraEntity entity) : base(entity) 
	{
		angleBoundary = 45f;
	}

	protected override float adjustAngle(float angle) {
		return angle;
	}

	protected override bool canShoot(float angle) {
		var t = angle / angleBoundary;
		if (t == 315 || t == 270) {
			return true;
		}
		else return false;
	}

}
