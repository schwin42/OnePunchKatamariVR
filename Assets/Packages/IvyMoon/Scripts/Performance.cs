using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IvyMoon;
/* 
Dictate how certain things run for performance reasons
*/

public class Performance : MonoBehaviour {
    public int maxDebris = 250; //amount of debris to be active at once
    public int fadeBuffer = 250; // Allows fade out for this amount of debris over the limit
    [HideInInspector] //hide the following list from inspector
    public List<GameObject> debris = new List<GameObject>(); // create list of gameobjects

    void RemoveDebris() {
        for (int i = 0; i < debris.Count; i++) {
            if (debris.Count > maxDebris + fadeBuffer)
            {
                DestroyObject(debris[i]);
               // debris[i].gameObject.GetComponent<Debris>().lifetime = 0; // destroy it by setting to zero
                                                                          //    break;
            }
            else {
                debris[i].gameObject.GetComponent<Debris>().lifetime = .5f; //change lifetime in the debris script instead of destroying so it will fade out still
            }
  //          debris.RemoveAt(i);


            }
        }
    void RemoveNull() {

        for (int i = 0; i < debris.Count; i++){
            if (debris[i].gameObject == null){
                debris.RemoveAt(i);
                     break;
            }
        }
    }
    // Update is called once per frame
    void Update() {
        if (debris.Count > maxDebris){
            RemoveDebris();
        }
        else {
            RemoveNull();
        }

    }
}