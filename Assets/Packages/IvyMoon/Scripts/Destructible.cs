using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
Destructible

Anything that you want to use to damage the destructible must be tagged as "Weapon"

]]WARNING[[
!!!You need to have(create if you dont) a TAG called "OnHit_SFX" in your project!!!
!!!You need to have(create if you dont) a TAG called "Weapon" in your project!!!


*/
namespace IvyMoon //this creates a namespace for all of the IvyMoon scripts so they dont interfere with yours
{

    public class Destructible : MonoBehaviour
    {
        public float hitsToKill = 1; //How many hits to kill the Destructible
        public bool experience;//allows Destructible to use simple Experience system.(done at startup, cannot update live.)
        public float XP; //set the amount of XP given on destruction
        public bool onHitColorChange;//allows Destructible to change color when hit
        public Color onHitColor; // pick the color the destructible changes to on hit
        public AudioClip onHitSound;//select a sound to play on hit. This sound will be randomly manipulated with pitch/volume to add varriation
        public GameObject onHitFX; //Set FX to play at the contact point of the Weapon hit
        public float onHitFXOffset;//off set the y axis to better portray your players height. 
        public GameObject deathFX; //set FX to play on destruction i.e. puff of smoke
        public float deathFXHeight;// adjust death FX height
        public AudioClip[] deathSounds;//select any number of sounds to play on destruction, one will be picked at random to add varriation 
        public GameObject deathDebris;//Select the parent object that has all the parts you would like to spawn at destruction. Make sure these are disabled at startup if you're using the debris script.
        public GameObject spawnPickup;//Select GameObject to enable on Destruction
        public bool physics; //allows Destructible to use physics engine.(done at startup, cannot update live.)
        public float mass = 1;//directly control rigidbody mass (done at startup, cannot update live.)

        Experience xp; //reference to experience script
        AudioClip deathAudio; //reference to deathAudio selection
        Rigidbody rgdbdy; //need this if the player chooses to use physics
        Color[] originalColors; //array used to store the original material colors attached to the destructible

        float timer; //used to add delays
        float State; //used to create a state machine in update function

        // Use this for initialization
        void Start()
        {
            //allows Destructible to use physics engine.
            if (physics)
            {
                rgdbdy = gameObject.AddComponent<Rigidbody>(); // when you give destructible a rigidbody it will use unity's physics engine.
                rgdbdy.mass = mass; //adjust mass of the rigidbody
            }
            //locates the experience script that will be updated when awarded xp 
            if (experience)
            {
                xp = (Experience)GameObject.FindObjectOfType(typeof(Experience));
            }

            //get the Destructibles materials and use this info for color change
            originalColors = new Color[GetComponent<Renderer>().materials.Length];  //create new colors amount based on the material length(number of connected materials)
            for (int i = 0; i < GetComponent<Renderer>().materials.Length; i++)
            {   //for loop - for each material on the object
                originalColors[i] = GetComponent<Renderer>().materials[i].color; //store the original colors
            }
            //Pick a Random Destroy sound from the set
            deathAudio = deathSounds[Random.Range(0, deathSounds.Length)];
        }


        AudioSource PlayClipHere(AudioClip audioClip, Vector3 position)
        {
            GameObject tempGO = new GameObject("TempAudio"); // create temp object
            tempGO.gameObject.tag = "OnHit_SFX"; //use tag to find how many are playing ...          !!!You need to create a TAG called "OnHit_SFX" in your project!!!
            tempGO.transform.position = position; // set its position
            AudioSource ASource = tempGO.AddComponent<AudioSource>(); // add an audio source
            ASource.clip = audioClip; //attach the AudioClip to the new temp AudioSource
            ASource.volume = Random.Range(.8f, .1f); ; //adjust volume randomly to add variety to hit sounds
            ASource.pitch = Random.Range(.75f, 1.5f); //adjust pitch randomly to add variety to hit sounds
            if (GameObject.FindGameObjectsWithTag("OnHit_SFX").Length < 5)
            { //dont allow too many of the same sound tags
                ASource.Play(); // start the sound
            }
            else {
                Destroy(tempGO);
            }
            Destroy(tempGO, ASource.clip.length); // destroy object after clip duration, Cannot have a looping Audio Clip
            return ASource; // return the AudioSource reference
        }
        void Pickup()
        {
            GameObject pickupItem = (GameObject)Instantiate(spawnPickup);
            pickupItem.transform.position = transform.position;
            pickupItem.SetActive(true);

        }
        // Update is called once per frame
        void Update()
        {
            if (State == 1)
            {
                hitsToKill--; //subtract 1 everytime I'm hit, this will allow us to know when the final hit is given

                //Play Sound on Hit
                if (onHitSound)
                {
                    PlayClipHere(onHitSound, transform.position);// run function so pitch and volume can be adjusted
                }
                else {
                    Debug.LogError("onHitSound is Null! You need an AudioSource to play a sound on hit.");
                }

                // Change destructible color to represent getting hit.
                if (onHitColorChange)
                {
                    //set a timer so color change will be seen.
                    if (hitsToKill == 0)
                    {
                        timer = Time.time + .03f; //set to a lower time on last hit to sell the destruction better
                    }
                    else {
                        timer = Time.time + 0.15f;//set a bit higher to give player better chance to see color change from hit(non-kill hits)
                    }
                    //set the color change to the onHitColor
                    foreach (Material mat in GetComponent<Renderer>().materials)
                    {//all materials attached to the gameobject will be affected by color change
                        mat.color = onHitColor;
                    }
                }
                State = 2;
            }
            if (State == 2)
            {
                //change destructible color back to original color.
                if (timer <= Time.time)
                {
                    for (int i = 0; i < GetComponent<Renderer>().materials.Length; i++)
                    {
                        GetComponent<Renderer>().materials[i].color = originalColors[i];

                    }

                    State = 3;
                }
            }
            if (State == 3)
            {
                if (hitsToKill <= 0)
                {
                    //On Kill do the following:
                    AudioSource.PlayClipAtPoint(deathAudio, transform.position); //play an audio clip
                                                                                 //activate fx and place at the gameObject
                    if (deathFX)
                    {
                        GameObject FXref = GameObject.Instantiate(deathFX);
                        FXref.SetActive(true);
                        FXref.transform.position = transform.position;
                        Vector3 adjustit = FXref.transform.position;
                        adjustit.y = FXref.transform.position.y + deathFXHeight;
                        FXref.transform.position = adjustit + transform.up * 1;
                        FXref.transform.eulerAngles = transform.eulerAngles;
                    }
                    //spawn parts
                    GameObject parts = (GameObject)Instantiate(deathDebris);
                    parts.transform.position = transform.position;
                    parts.transform.rotation = transform.rotation;
                    parts.SetActive(true);
                    //spawn a pickup item
                    if (spawnPickup)
                    {
                        Pickup();
                    }
                    Destroy(gameObject); //Kill Object

                }

            }

        }
        void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Weapon"))
            {
                RaycastHit hit;
                if (Physics.Raycast(other.transform.position, other.transform.forward, out hit))
                {

                    //spawn fx
                    if (onHitFX)
                    {
                        GameObject FXref = GameObject.Instantiate(onHitFX) as GameObject;
                        FXref.transform.position = hit.point;
                        Vector3 yAxis = hit.point;
                        yAxis.y = hit.point.y + onHitFXOffset;

                        FXref.transform.position = yAxis;
                        FXref.SetActive(true);

                    }
                }
                //start hit "State" machine
                State = 1;
            }
        }



        void OnDestroy()
        {
            if (experience)
            {
                xp.xpTotal = xp.xpTotal + XP;
            }
        }
    }
}
/*---------------------WIP----------------------
 * Attempting to make a custom Inspector Window for Destructible
 *----------------------------------------------*/
//
//#if UNITY_EDITOR

//[CustomEditor(typeof(Destructible))]
//public class DestructibleEditor : Editor{
////	private SerializedObject newobj;
////	private SerializedProperty newProp;
//
////	void OnEnable(){
////		newobj = new SerializedObject(target);
////	}
//	public override void OnInspectorGUI(){
//		Destructible options = (Destructible)target;
//
//
////		EditorGUILayout.Foldout (
//		EditorGUILayout.FloatField ("Health", options.health);
//
//		 EditorGUILayout.Toggle("Experience", options.experience);
//		if (options.experience) { 
//		 EditorGUILayout.FloatField ("XP Amount", options.XP);
//		}
//
//		options.physics = EditorGUILayout.Toggle ("Physics", options.physics);
//		if (options.physics) {
//			options.mass = EditorGUILayout.FloatField("Mass", options.mass);
//		}
//
//		bool allowSceneObjects = !EditorUtility.IsPersistent (target);
//		options.deathFX = (GameObject)EditorGUILayout.ObjectField ("Death VFX", options.deathFX, typeof(GameObject), allowSceneObjects);
//
////		newProp = newobj.FindProperty ("meprop");
////		EditorGUILayout.PropertyField (newProp, new GUIContent ("melabel"));
////		newobj.ApplyModifiedProperties ();
//	}
//}
//
//#endif