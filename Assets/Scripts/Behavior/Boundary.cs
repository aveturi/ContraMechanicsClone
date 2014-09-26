using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	protected void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Bullet") {
			Bullet bullet = other.gameObject.GetComponent<Bullet>(); 
			Destroy (bullet.gameObject);
		}
		else {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			if(entity != null) {
				entity.Damage();
			}
		}
	}
}
