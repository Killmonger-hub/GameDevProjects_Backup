using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunEffects : MonoBehaviour 
{
	public float damage = 10f;
	public float range = 100f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;

	public GameObject impactEffect;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			RayShoot ();
		}
	}

	void RayShoot()
	{
		muzzleFlash.Play ();

		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
		{
			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, .5f);
		}
	}

	private IEnumerator DestroyBulletAfterTime (GameObject bullet, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy (bullet);
	}
}
