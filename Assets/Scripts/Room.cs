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

	public List<GameObject> Doors = new List<GameObject> ();
	public GameObject roomFloor;

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
		xLength = 0;
		zLength = 0;

	}

	//overloaded constructor room
	public Room(RoomType roomType, Vector3 leftDown, Vector3 leftUp, Vector3 rightDown, Vector3 rightUp, float xDist, float zDist, GameObject horizonalWall, GameObject verticalWall, GameObject floor)
	{
		RoomID = ++RoomIdCounter;
		type = roomType;
		BottomLeft = leftDown;
		BottomRight = rightDown;
		TopLeft = leftUp;
		TopRight = rightUp;
		xLength = xDist;
		zLength = zDist;

		///make the floor
		roomFloor =(GameObject) GameObject.Instantiate(floor, new Vector3(leftDown.x + xDist / 2, 0.01f,leftDown.z + zDist /2), Quaternion.identity);
		roomFloor.transform.localScale = (new Vector3 (xDist, 0, zDist));
		Renderer roomFloorMat = roomFloor.transform.FindChild ("RoomFloor").GetComponent<Renderer>();
		roomFloorMat.material.mainTextureScale = new Vector2 (xDist, zDist);

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

	public void RemoveFloor()
	{
		GameObject.Destroy (roomFloor);
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

		leftWalls.Clear ();
		rightWalls.Clear ();
		topWalls.Clear ();
		bottomWalls.Clear ();
	}

	public void RemoveAllDoors()
	{
		foreach (GameObject g in Doors) 
		{
			GameObject.Destroy(g);
		}

		Doors.Clear ();
	}

	public bool AgainstWall(ItemDirection direction, Vector3 bl, int xD, int zD)
	{
		switch (direction) 
		{
		case ItemDirection.North:
			if(bl.z == TopLeft.z)
				return true;
			break;
		case ItemDirection.East:
			if(bl.x == TopRight.x)
				return true;
			break;
		case ItemDirection.South:
			if(bl.z == BottomLeft.z)
				return true;
			break;
		case ItemDirection.West:
			if(bl.x == TopLeft.x)
				return true;
			break;

		}

		//Debug.Log ("door not valid " + direction);

		return false;
	}

	public void AddDoorToList(GameObject door)
	{
		Doors.Add (door);
	}

	public void DisableWallsForDoors(ItemDirection direction,Vector3 bl, int xD, int zD )
	{
		Vector3 otherDoorPos;
		Vector3 firstDoorPos;

		Debug.Log (xD + "  " + zD);

		switch(direction)
		{
		case ItemDirection.West:

			otherDoorPos = new Vector3(bl.x, bl.y, bl.z);
			firstDoorPos = new Vector3(bl.x, bl.y, bl.z - xD);

		foreach (GameObject t in leftWalls)
		{
				if(t.transform.position == firstDoorPos || t.transform.position == otherDoorPos )
				t.gameObject.SetActive(false);

		}
			break;
		case ItemDirection.East:

			otherDoorPos = new Vector3(bl.x, bl.y, bl.z + xD);
			firstDoorPos = new Vector3(bl.x, bl.y, bl.z + zD);


		foreach (GameObject t in rightWalls)
		{
				if(t.transform.position == firstDoorPos || t.transform.position == otherDoorPos)
				t.gameObject.SetActive(false);
		}

			break;

		case ItemDirection.South:
		//working
			otherDoorPos = new Vector3(bl.x + zD, bl.y, bl.z);

		foreach (GameObject t in bottomWalls)
		{
			if(t.transform.position == bl || t.transform.position == otherDoorPos)
				t.gameObject.SetActive(false);
		}

			break;

		case ItemDirection.North:

		otherDoorPos = new Vector3(bl.x - zD, bl.y, bl.z);
		firstDoorPos = new Vector3(bl.x - xD, bl.y, bl.z);

		foreach (GameObject t in topWalls)
				if (t.transform.position == firstDoorPos || t.transform.position == otherDoorPos)
				t.gameObject.SetActive (false);

			break;

		}
	}


}
