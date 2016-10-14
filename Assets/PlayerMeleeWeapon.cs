using UnityEngine;
using System.Collections;

public class PlayerMeleeWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.collider.gameObject.layer == DungeonMaster.Instance.ProjectileHostileLayer) {
			Deflect (hit.collider.gameObject);
		}
	}

	private void Deflect(GameObject projectileGo) {


		Projectile projectile = projectileGo.GetComponent<Projectile> ();
		if (projectile.CurrentAlignment != Projectile.Alignment.Friendly) {
			projectile.CurrentAlignment = Projectile.Alignment.Friendly;
			Rigidbody rb = projectileGo.GetComponent<Rigidbody> ();
			rb.velocity *= -1;
		}


	}
}
