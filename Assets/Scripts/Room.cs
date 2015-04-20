using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum RoomType 
{ 
	None,
	General
}

public class Room {
		
	public static int RoomIdCounter = 0;

	public int RoomID;

	public RoomType type;

	public Vector3 BottomLeft;
	public Vector3 BottomRight;
	public Vector3 TopLeft;
	public Vector3 TopRight;

	public float xLength;
	public float zLength;

	public List<GameObject> leftWalls = new List<GameObject> ();
	public List<GameObject> rightWalls = new List<GameObject>();
	public List<GameObject> topWalls = new List<GameObject>();
	public List<GameObject> bottomWalls = new List<GameObject>();

	public const int MinXDist = 2;
	public const int MinZDist = 2;


	//construtor room
	public Room()
	{
		RoomID = ++RoomIdCounter;
		type = RoomType.None;
		BottomLeft = new Vector3 ();
		BottomRight = new Vector3 ();
		TopLeft = new Vector3 ();
		TopRight = new Vector3 ();
		//Middle = new Vector3 ();
		xLength = 0;
		zLength = 0;

	}

	//overloaded constructor room
	public Room(RoomType roomType, Vector3 leftDown, Vector3 leftUp, Vector3 rightDown, Vector3 rightUp, float xDist, float zDist, GameObject horizonalWall, GameObject verticalWall)
	{
		RoomID = ++RoomIdCounter;
		type = roomType;
		BottomLeft = leftDown;
		BottomRight = rightDown;
		TopLeft = leftUp;
		TopRight = rightUp;
		xLength = xDist;
		zLength = zDist;

		Debug.Log (xLength + " " + zLength);

		//build the walls

		///build top wall
		for (int a = 0; a <xLength; a ++) 
		{
			GameObject Topwall = GameObject.Instantiate(horizonalWall);
			Topwall.transform.position = new Vector3(TopLeft.x + a,TopLeft.y, TopLeft.z);
			Topwall.name = "top " + a;
			topWalls.Add (Topwall);
			
		}
		
//		//Build Bottom wall
//		
		for (int a = 0; a < xLength; a ++) 
		{
			GameObject BottomWall = GameObject.Instantiate(horizonalWall);
			BottomWall.transform.position = new Vector3(BottomLeft.x + a,BottomLeft.y, BottomLeft.z);
			BottomWall.name = "Bottom " + a;
			bottomWalls.Add (BottomWall);
			
		}
//		
//		//build Left wall
//		
		for (int a = 0; a < zLength; a ++) 
		{
			GameObject leftWall = GameObject.Instantiate(verticalWall);
			leftWall.transform.position = new Vector3(TopLeft.x,TopLeft.y, TopLeft.z - a);
			leftWall.name = "Left " + a;
			leftWalls.Add (leftWall);

		}
		
		//build Right Wall
		
		for (int a = 0; a < zLength; a ++) 
		{
			GameObject rightWall = GameObject.Instantiate(verticalWall);
			rightWall.transform.position = new Vector3(TopRight.x,TopRight.y, TopRight.z - a);
			rightWall.name = "right " + a;
			rightWalls.Add (rightWall);
		}

//		GameObject.Instantiate (cube, TopLeft, Quaternion.identity).name = "TOPLEFT";
//		GameObject.Instantiate (cube, TopRight, Quaternion.identity).name = "TOPRIGHT";
//		GameObject.Instantiate (cube, BottomLeft, Quaternion.identity).name = "BOTTOMLEFT";
//		GameObject.Instantiate (cube, BottomRight, Quaternion.identity).name = "BOTTOMRIGHT";

	}

	public void RemoveAllWalls()
	{
		foreach (GameObject go in leftWalls)
		{
			GameObject.Destroy(go);
		}

		foreach (GameObject go in rightWalls) 
		{
			GameObject.Destroy(go);
		}

		foreach (GameObject go in topWalls) 
		{
			GameObject.Destroy(go);
		}

		foreach (GameObject go in bottomWalls) 
		{
			GameObject.Destroy(go);
		}
	}

	public bool ValidForDoor(int xPos, int zPos)
	{
		//check top wall

		Debug.Log (RoomID);
		foreach (GameObject go in topWalls) 
		{

			Debug.Log("compare xPos: " + xPos + " " + go.transform.position.x + " zPos: " + zPos + 0.5f + " " + go.transform.position.z);

			if(xPos == go.transform.position.x && zPos == go.transform.position.z + 0.5f)
			{
				Debug.Log("found it on top row");
				return true;
			}
		}
		return false;
	}

}
