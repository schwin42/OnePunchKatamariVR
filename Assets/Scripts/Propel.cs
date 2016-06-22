using UnityEngine;
using System.Collections;

public class Propel : MonoBehaviour
{

	enum Mode
	{
		Ready,
		Yanking,
		Holding,
	}

	//Constants
	const float LERP_DURATION = 0.2F;
	const float ACCELERATION_MAGNITUDE = 5000;

	//Status
	Mode currentMode = Mode.Ready;
	float lerpProgress = 0f;
	Rigidbody target = null;
	RaycastHit currentHit;
	Vector3 targetStartPosition;
	Vector3 grabbedEdgeToOrigin;

	//Inspector
	public Rigidbody attachPoint;

	//Runtime
	SteamVR_TrackedObject trackedObject;
	LineRenderer lineRenderer;
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
		lineRenderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObject.index);

		//Draw line
		var t = reference;
		if (t == null)
			return;
		float refY = t.position.y;

		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out currentHit, 100000))
		{
			//Draw line
			lineRenderer.enabled = true;
			lineRenderer.SetPositions(new Vector3[] { ray.origin, currentHit.point });
			
		}
		else
		{
			lineRenderer.enabled = false;
		}

		switch (currentMode)
		{
			case Mode.Ready:
				if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
				{
					//print("input received");
					IUserInteractable iUserInteractable = currentHit.collider.GetComponent<IUserInteractable>();
					if(iUserInteractable != null)
					{
						iUserInteractable.Click();	
					} else
					{
						BeginYank();
					}

				}
				break;
			case Mode.Yanking:
				lerpProgress += Time.deltaTime;
				//target.MovePosition(Vector3.Lerp(targetStartPosition, attachPoint.position, lerpProgress / LERP_DURATION));
				target.MovePosition(Vector3.Lerp(targetStartPosition, attachPoint.position + grabbedEdgeToOrigin, lerpProgress / LERP_DURATION)); // Attempted to implement grab object by edge; seems closer than naive approach
				if (lerpProgress >= LERP_DURATION)
				{ //Magic number of seconds that yank lasts
					CompleteYank();
				}
				break;
			case Mode.Holding:
				if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
				{
					Throw();
				}
				else
				{
					target.MovePosition(attachPoint.position);

				}
				break;
		}
	}

	private void BeginYank()
	{
		print("begin yank");
		//Identify target object
		target = currentHit.rigidbody;
		if (target == null) return;

		//Begin lerp of target to endpoint
		lerpProgress = 0;
		currentMode = Mode.Yanking;
		targetStartPosition = target.position;
		grabbedEdgeToOrigin = target.position - currentHit.point;

	}

	private void CompleteYank()
	{
		currentMode = Mode.Holding;
		target.isKinematic = true;
		target.transform.SetParent(attachPoint.transform);
	}

	private void Throw()
	{
		currentMode = Mode.Ready;
		target.transform.SetParent(null);
		target.isKinematic = false;
		target.AddForce(transform.forward * ACCELERATION_MAGNITUDE, ForceMode.Acceleration);
		target = null;
	}

}
