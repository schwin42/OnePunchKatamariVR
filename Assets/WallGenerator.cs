using UnityEngine;
using System.Collections;
using Valve.VR;
using System.Collections.Generic;

public class WallGenerator : MonoBehaviour {

	public const float WALL_HEIGHT = 3;
	public const float WALL_THICKNESS = 0.01F;

	public GameObject wallPrefab;

//	[System.Serializable]
//	public HmdQuad_t quad;

	private SteamVR_PlayArea playArea;

	// Use this for initialization
	void Start () {
	
		playArea = GameObject.FindObjectOfType<SteamVR_PlayArea> ();

		BuildAllWalls ();
//		playAreaVertices = GetVector3VerticesOfPlayArea();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private List<Vector3> GetVector3VerticesOfPlayArea() {
		HmdQuad_t quad = new HmdQuad_t ();
		SteamVR_PlayArea.GetBounds(playArea.size, ref quad);
		return new List<Vector3>() {
			GetVector3FromHmdVector (quad.vCorners0),
			GetVector3FromHmdVector (quad.vCorners1),
			GetVector3FromHmdVector (quad.vCorners2),
			GetVector3FromHmdVector (quad.vCorners3)
		};
	}

	private Vector3 GetVector3FromHmdVector(HmdVector3_t hmdVector3) {
		Vector3 output = new Vector3 (hmdVector3.v0, hmdVector3.v1, hmdVector3.v2);
		print (output);
		return output;
	}

	private void BuildAllWalls() {
		Vector3 cameraOrigin = playArea.transform.position;

		List<Vector3> playAreaVerts = GetVector3VerticesOfPlayArea ();

		float midHeight = WALL_HEIGHT / 2;
		float yScale = WALL_HEIGHT;

		BuildWall (new Vector3 (cameraOrigin.x, midHeight, playAreaVerts [0].z),
			new Vector3 (playAreaVerts [0].x * 2, yScale, WALL_THICKNESS)
		);
//		BuildWall (new Vector3 (cameraOrigin.x, midHeight, playAreaVerts [2].z),
//			new Vector3 (playAreaVerts [0].x * 2, yScale, WALL_THICKNESS)
//		);
		BuildWall (new Vector3 (playAreaVerts[0].x, midHeight, cameraOrigin.z),
			new Vector3 (WALL_THICKNESS, yScale, playAreaVerts[0].z * 2)
		);
		BuildWall (new Vector3 (playAreaVerts[1].x, midHeight, cameraOrigin.z),
			new Vector3 (WALL_THICKNESS, yScale, playAreaVerts[0].z * 2)
		);
		BuildWall (new Vector3 (cameraOrigin.x, WALL_HEIGHT, cameraOrigin.z),
			new Vector3 (playAreaVerts[0].x * 2, WALL_THICKNESS, playAreaVerts[0].z * 2)
		);
		BuildWall (new Vector3 (cameraOrigin.x, 0, cameraOrigin.z),
			new Vector3 (playAreaVerts[0].x * 2, WALL_THICKNESS, playAreaVerts[0].z * 2)
		);


//
//		float zPosition = quad.vCorners3.v2;
//		float zScale = (quad.vCorners3.v2 - cameraOrigin.z) * 2 * 0.1F ;
//
//		Vector3 eulerAngles = new Vector3 (90, 0, 0);
//

//
//		float xPosition = cameraOrigin.x;
//		float xScale = (quad.vCorners1.v0 - cameraOrigin.x) * 2 * 0.1F;
//
//		BuildWall(new Vector3(xPosition, yPosition, zPosition),
//			eulerAngles,
//			new Vector3(xScale, yScale, zScale));
		//XY



//		BuildWall(

		//XZ

		//YZ
	}

	private void BuildWall(Vector3 position, Vector3 scale) {
		GameObject wall = Instantiate (wallPrefab) as GameObject;
		wall.transform.position = position;
		wall.transform.localScale = scale;
		wall.transform.SetParent (transform);
	}
}
