using UnityEngine;
using System.Collections;
using IvyMoon;
/*
 *Simple Display for public scripts
 */
public class DebugDisplay : MonoBehaviour {
	currency coins;//reference the currency script and give it a name in this script
	Experience xp;//reference the Experience script and give it a name in this script
    Performance Perf;//reference the Performance script

    public bool debrisCount;

// Use this for initialization
	void Start () {		
		//find the Experience script and set it to be referenced as xp in this script
		xp = (Experience)GameObject.FindObjectOfType (typeof(Experience));
		//find the currency script and set it to be referenced as coins in this script
		coins = (currency)GameObject.FindObjectOfType (typeof(currency));
        //
        Perf = (Performance)GameObject.FindObjectOfType(typeof(Performance));
    }
	void OnGUI() {
		GUILayout.Label ("Coins:" + coins.coins.ToString ("f2"));//print "Coins:" and the reference coins value of the public int coins to a string to be printed(coins value)
		GUILayout.Label ("Player_Level:" + xp.currentLevel.ToString ("f2"));
       if (debrisCount)  GUILayout.Label("Debris_Count:" + Perf.debris.Count.ToString("f2"));

    }
}