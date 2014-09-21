using UnityEngine;
using System.Collections;

public class BillController : Controller {
	
	private KeyCode		jumpKey = KeyCode.X;
	private KeyCode		shootKey = KeyCode.Z;
	
	public BillController (ContraEntity entity) : base(entity) 
	{
	}

	public override void Run () {

		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		float verticalAxis = Input.GetAxisRaw ("Vertical");
		Vector2	dir = new Vector2(entity.leftOrRight, 0);

		if (horizontalAxis > 0) {
			entity.MoveRight();
			entity.leftOrRight = 1;
			
		} else if (horizontalAxis < 0) {
			entity.MoveLeft();
			entity.leftOrRight = -1;

		}

		dir.Set (entity.leftOrRight, 0);

		if (verticalAxis < 0) {
			if (horizontalAxis != 0) {
				dir.y = -1;
			}
			else {
				entity.Crouch();
			}
		} 
		else {
			entity.Uncrouch();
		}

		if (verticalAxis > 0) {
			if (horizontalAxis != 0) {
				dir.y = 1;
			}
			else {
				dir.Set (0, 1);
			}
		}

		if (Input.GetKeyDown (jumpKey)) {
			entity.Jump();
		}


		if (Input.GetKeyDown (shootKey)) {
			// shoot logic
			//Debug.Log("Shoot!");
			entity.Shoot();
		} else if (Input.GetKey (shootKey)) {
			//Debug.Log("If i have an MGun, continuously shoot!");
			Bill bill = entity as Bill; 
			if(bill.gun.GetType().ToString() == "MGun"){
				entity.Shoot();
			}
		}

		entity.dir = dir;
	}
}
