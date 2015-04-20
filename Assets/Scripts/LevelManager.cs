using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Enum mode
public enum Mode
{
	None,
	RoomCreation,
	RoomFurnishing,
	RoomDeletion,
	StaffHiring,
	RoomDoors
}

// Enum state
public enum State
{
	None,
	Purchase,
	Placement,
	Choose,
	Deletion,
	Move
}


public class LevelManager : MonoBehaviour {

	//public static vars
	public static Mode GameMode = Mode.None;
	public static State GameState = State.None;
	public static RoomType roomType = RoomType.None;

	public GFGrid grid;
	public Transform HoverGO;
	private bool IsDragging;

	public GameObject cubePrefab;
	public Transform DraggedPos;

	private Vector3 MiddleDragPos;

	private Vector3 selectedCell;

	public GameObject Selecto;
	private float xDist;
	private float zDist;

	public GameObject WallPrefab;
	public GameObject DoorPrefab;

	private bool canBuild;
	private bool DoorSpawned;

	int tileWidth = 1;

	int tileHeight = 1;

	private GameObject Door = null;


	public List<Room> rooms;

	Vector3 TopLeft = new Vector3(0,0,0);
	Vector3 TopRight = new Vector3(0,0,0);
	Vector3 BottomLeft = new Vector3(0,0,0);
	Vector3 BottomRight = new Vector3(0,0,0);

	private Room SelectedRoom;


	
	
	void Awake()
	{
		rooms = new List<Room> ();
		HoverGO = Instantiate (HoverGO);

	}

	void Update()
	{
		///we are placing a room state
		if (GameMode == Mode.RoomCreation && GameState == State.Placement) {

			ChoosingRoomPlacement ();

		} else if (GameMode == Mode.RoomDeletion && GameState == State.Choose) {

			RoomSelection ();

		} else if (GameMode == Mode.RoomDeletion && GameState == State.Deletion) {

			//remove the room from the list
			rooms.Remove (SelectedRoom);

			///remove all walls
			SelectedRoom.RemoveAllWalls ();

			///reset the maps
			Maps.setFloorMapBlock (SelectedRoom.BottomLeft, SelectedRoom.xLength, SelectedRoom.zLength, 0);
			Maps.setRoomMapBlock (SelectedRoom.BottomLeft, SelectedRoom.xLength, SelectedRoom.zLength, 0);

			SelectedRoom = null;
			Selecto.SetActive (false);
			HoverGO.gameObject.SetActive (false);

			GameMode = Mode.None;
			GameState = State.None;
		} else if (GameMode == Mode.RoomDoors && GameState == State.Placement) 
		{
			PlaceRoomDoors();
		}
		else
		{
			///put in to stop here

			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100.0f)) {
				HoverGO.position = getTilePoints(hit.point);
			}
		}
	}

	void ChoosingRoomPlacement()
	{

		Ray ray;
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if(Physics.Raycast(ray, out hit, 100.0f))
		{
			HoverGO.position = getTilePoints(hit.point);

			if(Input.GetMouseButtonDown(0))
			{
				//store the first postion
				selectedCell = HoverGO.position;
				Selecto.SetActive (true);
			}

			if(Input.GetMouseButtonUp(0))
			{
				selectedCell = new Vector3(0,0,0);
				BuildRoom();
			}

			///find distance to drag point
			DraggedPos.position = getTilePoints(hit.point);
		

			/// measure distance between these objects
			 xDist = selectedCell.x - DraggedPos.position.x;
			 zDist = selectedCell.z - DraggedPos.position.z;


			//set the middle point
			MiddleDragPos = new Vector3(selectedCell.x - xDist / 2,0,selectedCell.z - zDist / 2);

			///move the Scale pice into pos
			Selecto.transform.position = MiddleDragPos;
			Selecto.transform.localScale = new Vector3(xDist,0.1f, zDist);

			
			print (selectedCell.x + " topleft " + (selectedCell.x - xDist) + " TopRight ");
			print (selectedCell.z + " bottomleft " + (selectedCell.z - zDist) + " BottomRight ");

			
			///get the correct Position of the corner cubes
			
			if (selectedCell.x < selectedCell.x - xDist && selectedCell.z > selectedCell.z - zDist) {
//				print ("from left to the right, up to down");
				
				TopLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
				TopRight = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
				BottomLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
				BottomRight = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
				
			} else if (selectedCell.x > selectedCell.x - xDist && selectedCell.z > selectedCell.z - zDist) 
			{
//				print ("from right to left up to down");
				
				TopRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
				TopLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
				BottomRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
				BottomLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
				
			} else if (selectedCell.x < selectedCell.x - xDist && selectedCell.z < selectedCell.z - zDist)
			{
//				print ("form left to right down to up");
				
				BottomLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
				BottomRight= new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
				TopLeft= new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
				TopRight= new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
				
			} else if (selectedCell.x > selectedCell.x - xDist && selectedCell.z < selectedCell.z - zDist)
			{
//				print ("form right to left down to up");
				
				BottomRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
				BottomLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
				TopRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
				TopLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
			}



			if(!isBlockSumZero(BottomLeft, xDist,zDist) || !isRoomSizeCorrect(xDist,zDist))
			{
				Selecto.GetComponent<Renderer>().material.color = Color.red;
				canBuild = false;
			}
			else
			{
				Selecto.GetComponent<Renderer>().material.color = Color.green;
				canBuild = true;
			}
		}
	}
	

	void BuildRoom()
	{
		Selecto.SetActive (false);

		if (!canBuild)
			return;


		///make a room with the walls and add it to the maps
	
//		Room room = new Room (RoomType.General, BottomLeft, TopLeft, BottomRight, TopRight, xDist, zDist, MiddleDragPos,cubePrefab);
//		rooms.Add (room);
//
//		Maps.setFloorMapBlock (BottomLeft, xDist, zDist, 2);
//		Maps.setRoomMapBlock (BottomLeft, xDist, zDist, room.RoomID);
//
//		GameMode = Mode.RoomDoors;
//		GameState = State.Placement;

	}

	void PlaceRoomDoors ()
	{
		// make a door
		if(!DoorSpawned)
		{
			SpawnDoor();
		}

		HoverGO.localScale = new Vector3 (1, 0.1f, 1);
		
		Ray ray;
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100.0f)) 
		{
			Door.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
			grid.AlignTransform(Door.transform);

			/// get last id of room from the list
			int lastRoomID = rooms[rooms.Count - 1].RoomID;

			if(Maps.GetRoomID((int)Door.transform.position.x, (int)Door.transform.position.z) == lastRoomID)
			{

				if(rooms[lastRoomID - 1].ValidForDoor((int)Door.transform.position.x, (int)Door.transform.position.z))
				{
					print ("done");
				}

			}
		}
	
	}

	void SpawnDoor()
	{
		Door = Instantiate (DoorPrefab);
		DoorSpawned = true;
	}

	void RoomSelection ()
	{
		HoverGO.localScale = new Vector3 (1, 0.1f, 1);
		
		Ray ray;
		RaycastHit hit;
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100.0f)) 
		{
			HoverGO.position = new Vector3(hit.point.x, 0, hit.point.z);
			grid.AlignTransform(HoverGO);

				
			///get the room id
			int selectedRoomID = Maps.GetRoomID((int)HoverGO.position.x, (int)HoverGO.position.z);

			if(selectedRoomID == 0)
			{
				SelectedRoom = null;
				return;
			}

			foreach(Room r in rooms)
			{
				if(r.RoomID == selectedRoomID)
				{
					SelectedRoom = r;
					break;
				}
			}

			///move the Scale pice into pos
			Selecto.SetActive(true);
			Selecto.GetComponent<Renderer>().material.color = Color.green;
			//Selecto.transform.position = SelectedRoom.Middle;
			Selecto.transform.localScale = new Vector3(SelectedRoom.xLength,0.1f, SelectedRoom.zLength);

			if(Input.GetMouseButton(0) && SelectedRoom != null)
			{
				GameState = State.Deletion;
			}

		}
	}

	bool isRoomSizeCorrect(float xRange, float zRange)
	{
		///round it to whole number
		xRange = Mathf.Abs (xRange);
		zRange = Mathf.Abs (zRange);

		if (xRange > Room.MinXDist && zRange > Room.MinZDist)
		{
			return true;
		}

		return false;
	}

	bool isBlockSumZero(Vector3 leftBottom, float xRange, float zRange)
	{
		///round it to whole number
		xRange = Mathf.Abs (xRange);
		zRange = Mathf.Abs (zRange);

		int sum = 0;

		for (int a = 0; a < xRange; a++)
		{
			for(int b = 0; b < zRange; b++)
			{
				sum = sum + Maps.GetFloorMapValue((int)leftBottom.x + a, (int)leftBottom.z + b);
			}
		}

		if (sum == 0) {
//			
			return true;
		} else {

			return false;
		}
	}

	// Convert world space floor points to tile points
	Vector3 getTilePoints(Vector3 floorPoints)
	{
		Vector3 tilePoints = new Vector3();
		
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / tileWidth);
		tilePoints.z = (int)(floorPoints.z / tileHeight);
		tilePoints.y = 0;
		
		// Return the tile points
		return tilePoints;
	}


}
