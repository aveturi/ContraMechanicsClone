using UnityEngine;
using System.Collections;

public abstract class ContraEntity : MonoBehaviour {

	// Change to public
	protected float 		health;
	protected Controller	controller;
	public Vector2		dir {get; set;}
	public int				leftOrRight; // [1 = Right, -1 = Left]
	public int				upOrDown; // [1 = up, -1 = down]
	public virtual void SetDirection(Vector2 dir) {
		this.dir = dir;
	}

	public virtual void MoveLeft() {}
	public virtual void MoveRight() {}
	public virtual void Jump() {}
	public virtual void FallThrough() {}
	public virtual void Crouch() {}
	public virtual void Uncrouch() {}
	public virtual void Shoot() {}
	public virtual void Damage(int damageTaken = 0) {}
}
