using UnityEngine;
using System.Collections;

public abstract class ContraEntity : MonoBehaviour {

	// Change to public
	public int 				health;	// Or lives
	protected Controller	controller;
	public Vector2			dir {get; set;}
	public int				leftOrRight; // [1 = Right, -1 = Left]
	public int				upOrDown; // [1 = up, -1 = down]
	public GameObject   	bulletPrefab;
	protected float 		bulletDeltaSpace = 0.3f;
	
	public virtual void MoveLeft() {}
	public virtual void MoveRight() {}
	public virtual void Jump() {}
	public virtual void FallThrough() {}
	public virtual void Crouch() {}
	public virtual void Uncrouch() {}
	public virtual void Shoot() {}
	public virtual void Damage(int damageTaken = 0) {}
}
