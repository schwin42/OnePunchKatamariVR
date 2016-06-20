using UnityEngine;
using System.Collections;
using IvyMoon;
/*
 *Simple XP multiplier 
 */
public class Experience : MonoBehaviour {
	public float xpTotal; //total amount of xp
	public int currentLevel;//the current level

	float levelUpAmount = 100; //change this to require more XP to level up

	// Update is called once per frame
	void Update () {
		if (xpTotal > levelUpAmount) {
			currentLevel++; //other ways to get same result: currentLevel = currentLevel + 1; or currentLeve+=1;
			levelUpAmount = levelUpAmount *2; //multiply levelup amount so next level takes longer to achieve
			}
		}
}
