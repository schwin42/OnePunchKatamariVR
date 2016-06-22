using UnityEngine;
using System.Collections;

public class CustomTeleporter : MonoBehaviour
{

	SteamVR_TrackedObject trackedObject;


	LineRenderer lineRenderer;
	HitReticle hitReticle;

	Transform reference
	{
		get
		{
			var top = SteamVR_Render.Top();
			return (top != null) ? top.origin : null;
		}
	}

	void Awake()
	{
		trackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start()
	{
		hitReticle = GetComponent<HitReticle>();
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.enabled = false;


	}

	void Update()
	{
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObject.index);
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		//if(device.GetTouch(SteamVR_Controller.ButtonMask.Axis0))
		{
			print("touchpad received");

			//Display destination indicator + line 

			//Draw line
			var t = reference;
			if (t == null)
				return;
			float refY = t.position.y;

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100000))
			{
				//Draw line
				lineRenderer.enabled = true;
				lineRenderer.SetPositions(new Vector3[] { ray.origin, hit.point });

			}
			else
			{
				DisableTeleportUi();
			}
		}

		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Teleport
			if (hitReticle.currentHit.HasValue)
			{
				transform.root.position = hitReticle.currentHit.Value.point;
			}

			DisableTeleportUi();
		}

	}
	private void DisableTeleportUi()
	{
		lineRenderer.enabled = false;
		//runtimeDestinationIndicator.SetActive(false);
	}
}
