using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using IvyMoon;

public class ResetScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public void restartCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update () {        
            if (Input.GetKey(KeyCode.F1)) {
            restartCurrentScene();
                    //Application.LoadLevel();
                }
    }
}
