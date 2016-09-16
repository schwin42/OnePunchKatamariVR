using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		var rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (10000, 1000, 100);
		print ("finished");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
