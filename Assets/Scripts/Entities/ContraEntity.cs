using UnityEngine;
using System.Collections;

public abstract class ContraEntity : MonoBehaviour {

	// Change to public
	protected float 		health;
	protected Controller	controller;

	public virtual void MoveLeft() {}
	public virtual void MoveRight() {}
	public virtual void Jump() {}
	public virtual void FallThrough() {}
	public virtual void Crouch() {}
	public virtual void Uncrouch() {}
	public virtual void Shoot() {}
	public virtual void Damage(int damageTaken = 0) {}
}
