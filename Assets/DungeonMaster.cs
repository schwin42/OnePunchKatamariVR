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

	// Use this for initialization
	void Start () {
	
		_hmdOrigin = GameObject.Find ("[CameraRig]").transform;
		_headsetOrigin = _hmdOrigin.Find ("Camera (eye)");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
