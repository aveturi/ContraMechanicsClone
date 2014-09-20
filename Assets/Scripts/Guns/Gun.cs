using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour {

	protected float	lastStep;
	protected int		bulletCount = 0;
	protected float 	timeBetweenSteps = 2f;
	protected	int		numMaxBullets = 4;
	public GameObject   	bulletPrefab;

	protected ContraEntity		entity;

	public virtual void Shoot(){}
}
