using UnityEngine;
using System.Collections;

public class CamoSniperController : Controller {


	public CamoSniperController (ContraEntity entity) : base(entity) 
	{
	}


	
	public override void Run () {

		entity.Uncrouch ();

		
		var bill = GameObject.FindGameObjectWithTag ("BillRizer");
		if (bill == null) {
			return;
		} else {
			entity.leftOrRight = (bill.transform.position.x < entity.transform.position.x) ? -1 : 1;
			entity.dir = new Vector2(entity.leftOrRight, 0);

		}
		
		if (entity.leftOrRight < 1) {
			// do facing left animation
		} else {
			// do facing right animation
		}

		// adjust direction && shoot
		entity.Shoot ();

		entity.Crouch ();

	    
	}

}
