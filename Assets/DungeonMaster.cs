using UnityEngine;
using System.Collections;

public class DungeonMaster : MonoBehaviour {

	private static DungeonMaster _instance = null;
	public static DungeonMaster Instance { 
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<DungeonMaster> ();
			}
			return _instance;
		}
	}

	private Transform _hmdOrigin;
	public Transform HmdOrigin {
		get {
			return _hmdOrigin;
		}
	}

	private Transform _headsetOrigin;
	public Transform HeadsetOrigin {
		get {
			return _headsetOrigin;
		}
	}
		
	private int _projectileFriendlyLayer;
	public int ProjectileFriendlyLayer {
		get {
			return _projectileFriendlyLayer;
		}
	}

	private int _projectileHostileLayer;
	public int ProjectileHostileLayer {
		get {
			return _projectileHostileLayer;
		}
	}

	// Use this for initialization
	void Start () {
	
		_hmdOrigin = GameObject.Find ("[CameraRig]").transform;
		_headsetOrigin = _hmdOrigin.Find ("Camera (eye)");

		_projectileHostileLayer = LayerMask.NameToLayer ("ProjectileHostile");
		_projectileFriendlyLayer = LayerMask.NameToLayer ("ProjectileFriendly");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
