using UnityEngine;
using System.Collections;
using IvyMoon;
/*
Simple Particle Remover
Attaching this script will clean up any cloned particle.
This removes the particle when its done(no longer alive) and stops it from becoming a memory leak. 
*/
public class RemoveParticle : MonoBehaviour {
	ParticleSystem PS;//Create Particle System Reference named PS

	// Use this for initialization
	void Start () {
		PS = GetComponent<ParticleSystem> ();//Set PS to this objects Particle system
	}
	
	// Update is called once per frame
	void Update () {
		if (PS) {
			if (!PS.IsAlive ()) {//if IsAlive returns false on PS
				Destroy (gameObject);//Destroy myself
			}
		} else {
			Debug.LogError ("PS is not set to a Particle system, please remove this script. I'm not doing anything.");
		}
	}
}
