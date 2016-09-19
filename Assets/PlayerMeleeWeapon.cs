using UnityEngine;
using System.Collections;

public class PlayerMeleeWeapon : MonoBehaviour {

	private int projectileLayer = -1;

	// Use this for initialization
	void Start () {
	
		projectileLayer = LayerMask.NameToLayer ("Projectile");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.collider.gameObject.layer == projectileLayer) {
			Destroy (hit.collider.gameObject);
		}
	}
}
