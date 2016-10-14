using UnityEngine;
using System.Collections;

public class TestPiston : MonoBehaviour {

	public Vector3 velocity;
//	public float translateDuration;

//	private bool translating;

	private float progress = 0;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody> ();

//		if (!rb.isKinematic) {
//			rb.velocity = velocity;
//		}
	}
	
	// Update is called once per frame
	void Update () {

//		if (rb.isKinematic) {
			transform.Translate (velocity);
//		}

	
//		if (translating) {
//			progress += Time.deltaTime;
//			transform.Translate (direction * progress);
//			if (progress < translateDuration) {
//
//			} else {
//				
//			}
//		}
	}

//	void BeginTranslate() {
//		progress = 0;
//		translating = true;
//	}
}
