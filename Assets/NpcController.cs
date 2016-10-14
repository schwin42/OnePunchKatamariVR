using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterSheet))]

public class NpcController : MonoBehaviour {

	CharacterSheet characterSheet;

	// Use this for initialization
	void Start () {
	
		characterSheet = GetComponent<CharacterSheet> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision hit) {
		print ("1");
		if (hit.gameObject.layer == DungeonMaster.Instance.ProjectileFriendlyLayer) {
			print ("2");
			Projectile projectile = hit.gameObject.GetComponent<Projectile> ();
			characterSheet.Damage (projectile.damage);
		}
	}
}
