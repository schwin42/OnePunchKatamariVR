using UnityEngine;
using System.Collections;
using System.Linq;

public class HitReticle : MonoBehaviour
{

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
	public RaycastHit? currentHit;

	Propel propel;

	void Awake()
	{
		trackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start()
	{
		runtimeReticle = Instantiate<GameObject>(reticlePrefab);
		runtimeReticle.gameObject.transform.localScale = new Vector3(RETICLE_SCALE, RETICLE_SCALE, RETICLE_SCALE);

		propel = GetComponent<Propel>();
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
		RaycastHit[] hits = Physics.RaycastAll(ray, 100000);
		hits.OrderBy(h => h.distance);
		bool currentHitAssigned = false;
		print(hits.Length + "hits");
		foreach (RaycastHit hit in hits)
		{
			bool ignoreHit = false;
			foreach(Transform tr in propel.attachPoint.transform)
			{
				if (tr.gameObject == hit.collider.gameObject)
				{
					ignoreHit = true;
					break;
				}

			}
			if (ignoreHit) continue;

			//Draw destination indicator
			currentHit = hit;
			runtimeReticle.SetActive(true);
			runtimeReticle.transform.position = currentHit.Value.point;
			currentHitAssigned = true;
			break;
		}
		if (!currentHitAssigned)
		{
			runtimeReticle.SetActive(false);
			currentHit = null;
		}
	}
}
