using UnityEngine;
using System.Collections;

public class CharacterSheet : MonoBehaviour {

	//Stats
	public int maxHp = 10;

	//Status
	public int currentHp;


	// Use this for initialization
	void Start () {
	
		currentHp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (currentHp <= 0) {
			print ("4");
			Destroy (this.gameObject);
		}
	}

	public void Damage(int damage) {
		print ("3");
		currentHp -= damage;
	}
}
