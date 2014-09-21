using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Bullet") {
			Bullet bullet = other.gameObject.GetComponent<Bullet>(); 
			Destroy (bullet.gameObject);
		}
		else {
			ContraEntity entity = other.gameObject.GetComponent<ContraEntity>(); 
			if(other.tag != "PowerUp")
			entity.Damage();
		}
	}
}
