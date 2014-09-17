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

		if (horizontalAxis > 0) {
			entity.MoveRight();
			
		} else if (horizontalAxis < 0) {
			entity.MoveLeft();
		}

		if (verticalAxis < 0) {
			entity.Crouch();
		} else {
			entity.Uncrouch();
		}

		if (Input.GetKeyDown (jumpKey)) {
			entity.Jump();
		}
		
		if (Input.GetKeyDown (shootKey)) {
			entity.Shoot();
		}
	
	}
}
