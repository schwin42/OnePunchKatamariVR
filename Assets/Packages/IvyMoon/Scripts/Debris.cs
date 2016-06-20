using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IvyMoon;
/*
Debris

    ]]WARNING[[
Make sure the material you assign to the model uses Fade as the Rendering Mode. (Unity defaults to Opaque when making a new material)
If you dont then the models will pop out of existence instead of fade.

If the Debris are interacting with the player and you dont want this to happen:
    Create a new layer, Edit>ProjectSettings>Tags and Layers, called "Collision"(FYI layer naming doesnt matter) place all objects on this layer that you would like the debris to collide with (i.e. the ground)
    Create another layer, Edit>ProjectSettings>Tags and Layers, called "NoPlayerCollide". set the layer to "NoPlayerCollide" in the inspector window for each debris
    Change Layer settings (Edit>ProjectSettings>Physics). Unmark the checkbox for interaction between "Default" and "NoPlayerCollide"
    Now the player (who is set to Default layer in prefab) and the debris will not interact with eachother and the debris will still interact with the ground (not fall through).

If you want a different amount of debris scripts to be active at once:
    Change DebrisAlive.Length>200 in void Update to something besides 200
    This is set to 200 to avoid having too many active debris scripts at once. Change at your own risk.

*/
public class Debris : MonoBehaviour {
	public float lifetime = 2;// set time to activate fade out
	public float fadeSpeed = 1;// set how fast the fade is
	public int forceRange = 4;//set force intensity

    [HideInInspector] //use this to keep the timer hidden from inspector view, but still be a public variable.
    public float timer;// used to setup lifetime

    Color fade;//used to hold color reference
    Performance Perf;
    IEnumerator Fade(){
		//for loop subtracting based on time in seconds, not frames
		for (float f = 1f; f >= 0; f -= Time.deltaTime * fadeSpeed) { 
			fade = GetComponent<Renderer> ().material.color;//set fade to the current color(RGBA)
			fade.a = f;//change alpha of fade
			GetComponent<Renderer>().material.color = fade;//set current color(RGBA) to fade
			yield return null;
		}
	}
    void Start()
    {
        //uses cameras forward direction to determine which way the debris fly.
        //replace "Camera.main.transform.forward" with "whatYouWantToDetermineDirection.forward" if you dont want the camera to control this.
        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * forceRange, ForceMode.Impulse);

        //attempt to determine weapons forward....
        //gameObject.GetComponent<Rigidbody>().AddForce(.transform.forward * forceRange, ForceMode.Impulse);


    }
    void Awake(){
        Perf = (Performance)GameObject.FindObjectOfType(typeof(Performance));
        Perf.debris.Add(this.gameObject);
        }

	void Update () {
        //print (fade.a + "fadeAlpha");
        timer += Time.deltaTime;// keep timer based on seconds, not frames
		//kill when faded
		if(fade.a >0 && fade.a <=0.09f){//check that the fade is greater than zero(fade comes up as zero at startup) and less than .09
			Destroy (transform.parent.gameObject);//make sure gameobject containing all debris is destroyed
			Destroy (gameObject); //I've faded out kill me
		}
		//Begin fade out based on lifetime
		if (timer >= lifetime) {
			//Transparency.
			StartCoroutine("Fade");//Run the Ienumerator
		}

	}
    void OnDestroy() { Perf.debris.Remove(this.gameObject); }
}
