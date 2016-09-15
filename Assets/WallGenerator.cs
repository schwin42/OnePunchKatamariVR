using UnityEngine;
using System.Collections;
using Valve.VR;

public class WallGenerator : MonoBehaviour {

	public const float WALL_HEIGHT = 2;

	public GameObject wallPrefab;

	private SteamVR_PlayArea playArea;

	// Use this for initialization
	void Start () {
	
		playArea = GameObject.FindObjectOfType<SteamVR_PlayArea> ();

		BuildAllWalls ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void BuildAllWalls() {

		Vector3 cameraOrigin = playArea.transform.position;

		HmdQuad_t rect = new HmdQuad_t ();
		SteamVR_PlayArea.GetBounds(playArea.size, ref rect);
		float zPosition = rect.vCorners3.v2;
		float zScale = (rect.vCorners3.v2 - cameraOrigin.z) * 2 * 0.1F ;

		Vector3 eulerAngles = new Vector3 (90, 0, 0);

		float yPosition = WALL_HEIGHT / 2;
		float yScale = WALL_HEIGHT * 0.1F;

		float xPosition = cameraOrigin.x;
		float xScale = (rect.vCorners1.v0 - cameraOrigin.x) * 2 * 0.1F;

		BuildWall(new Vector3(xPosition, yPosition, zPosition),
			eulerAngles,
			new Vector3(xScale, yScale, zScale));
		//XY



//		BuildWall(

		//XZ

		//YZ
	}

	private void BuildWall(Vector3 position, Vector3 eulerAngles, Vector3 scale) {
		GameObject wall = Instantiate (wallPrefab) as GameObject;
		wall.transform.position = position;
		wall.transform.eulerAngles = eulerAngles;
		wall.transform.localScale = scale;
		wall.transform.SetParent (transform);
	}
}
