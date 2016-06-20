using UnityEngine;
using System.Collections;

public class HitReticle : MonoBehaviour {

	const float RETICLE_SCALE = 0.1F;

	public GameObject reticlePrefab;
	SteamVR_TrackedObject trackedObject;
	Transform reference
	{
		get
		{
			var top = SteamVR_Render.Top();
			return (top != null) ? top.origin : null;
		}
	}
	GameObject runtimeReticle;
	public RaycastHit currentHit;

	void Awake()
	{
		trackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		runtimeReticle = Instantiate<GameObject>(reticlePrefab);
		runtimeReticle.gameObject.transform.localScale = new Vector3(RETICLE_SCALE, RETICLE_SCALE, RETICLE_SCALE);
	}

	void FixedUpdate()
	{
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObject.index);
			//Display destination indicator + line 

			//Draw line
			var t = reference;
			if (t == null)
				return;
			float refY = t.position.y;

			Ray ray = new Ray(transform.position, transform.forward);
			if (Physics.Raycast(ray, out currentHit, 100000))
			{
				//Draw destination indicator
				runtimeReticle.transform.position = currentHit.point;
			}
			else
			{
				//DisableTeleportUi();
			}
	}
}
