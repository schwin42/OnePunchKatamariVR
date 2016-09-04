using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {
	
	public Material skyboxMat;

    // Use this for initialization
    void Start () {

		RenderSettings.skybox = skyboxMat;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
