using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour, IUserInteractable {

	public void Click()
	{
		SceneManager.LoadScene(0);
	}

	public void Update()
	{

		//transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}
