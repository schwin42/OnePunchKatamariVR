using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public enum Alignment
	{
		Hostile = 0,
		Friendly = 1
	}

	public int damage = 10;

	private Alignment _currentAlignment;
	public Alignment CurrentAlignment {
		get {
			return _currentAlignment; 
		}
		set {
			if (value == Alignment.Friendly) {
				gameObject.layer = DungeonMaster.Instance.ProjectileFriendlyLayer;
			} else if (value == Alignment.Hostile) {
				gameObject.layer = DungeonMaster.Instance.ProjectileHostileLayer;
			}

			_currentAlignment = value;
		}
	}

	// Use this for initialization
	void Start () {
	
		_currentAlignment = Alignment.Hostile;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
