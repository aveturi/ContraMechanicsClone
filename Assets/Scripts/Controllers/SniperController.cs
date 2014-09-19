using UnityEngine;
using System.Collections;

public class SniperController : Controller {

	private GameObject 		bill = null;
	
	public SniperController (ContraEntity entity) : base(entity) 
	{
		bill = GameObject.FindGameObjectWithTag ("BillRizer");
	}

	public override void Run () {

		if (bill == null) {
			return;
		} else {

			entity.leftOrRight = (bill.transform.position.x < entity.transform.position.x) ? -1 : 1;
			entity.upOrDown = (bill.transform.position.y < entity.transform.position.y) ? 1 : -1; 

			//TODO:for now sniper can shoot exactly at billRizer
			// but we should model his 8 shooting regions properly
			Vector2 shootDirection = bill.transform.position - entity.transform.position;
			entity.dir = shootDirection.normalized;

			entity.Shoot();
		}
	}
	
}

