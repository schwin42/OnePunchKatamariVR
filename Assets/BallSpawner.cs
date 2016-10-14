using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour {

	public GameObject ballPrefab;

	private const float LAUNCH_FORCE = 500;

	private const float SECONDS_BETWEEN_BALLS = 2.0F;

	private bool firingEnabled = true;

	// Use this for initialization
	void Start () {
	
		StartCoroutine (RepeatedlyFireBalls ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator RepeatedlyFireBalls() {
		while (firingEnabled) {
			FireBallAtHeadset ();
			yield return new WaitForSeconds(SECONDS_BETWEEN_BALLS);
		}
	}

	void FireBallAtHeadset() {

		GameObject ballInstance = Instantiate (ballPrefab, transform) as GameObject;
		ballInstance.transform.localPosition = Vector3.zero;
		Rigidbody rigidbody = ballInstance.GetComponent<Rigidbody> ();
//		print ("Firing ball in direction: " + (DungeonMaster.Instance.HeadsetOrigin.position - transform.position));
		Vector3 launchVector = (DungeonMaster.Instance.HeadsetOrigin.position - transform.position) * LAUNCH_FORCE;
		rigidbody.AddForce (launchVector);
	}
}
