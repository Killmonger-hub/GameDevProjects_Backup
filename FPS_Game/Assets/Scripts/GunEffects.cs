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

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float bulletSpeed = 30f;
	public float lifetime = 3f;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			RayShoot ();
			BulletShoot ();
		}
	}

	void RayShoot()
	{
		muzzleFlash.Play ();

		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
		{
			Debug.Log (hit.transform.name);

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, .5f);
		}
	}

	private void BulletShoot()
	{
		GameObject bullet = Instantiate (bulletPrefab);
		Physics.IgnoreCollision (bullet.GetComponent<Collider> (), bulletSpawn.parent.GetComponent<Collider> ());

		bullet.transform.position = bulletSpawn.position;

		Vector3 rotation = bullet.transform.rotation.eulerAngles;

		bullet.transform.rotation = Quaternion.Euler (rotation.x, transform.eulerAngles.y, rotation.z);

		bullet.GetComponent<Rigidbody> ().AddForce (bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

		StartCoroutine (DestroyBulletAfterTime (bullet, lifetime));
	}

	private IEnumerator DestroyBulletAfterTime (GameObject bullet, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy (bullet);
	}
}
