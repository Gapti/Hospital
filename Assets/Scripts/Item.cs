using UnityEngine;
using System.Collections;

public enum ItemDirection { North, South, East, West }
public enum ItemType { Door, Bench }

public class Item : MonoBehaviour {

	public static int itemIDGenerator = 0;

	public string ItemName;
	public Room AssignedRoom;
	public int widthInCells;
	public int heightInCells;
	public bool AgainstWall;


	private int itemID;
	public ItemDirection direction;
	public ItemType type;

	/// get door mats
	private GameObject mat1;
	private GameObject mat2;
	

	void Awake()
	{
		itemID = ++itemIDGenerator;
		direction = ItemDirection.South;

		if (type == ItemType.Door) 
		{
			mat1 = this.transform.FindChild ("Door Mat 1").gameObject;
			mat2 = this.transform.FindChild ("Door Mat 2").gameObject;
		}

	}
	
	public void Rotate()
	{
		ItemSwitch ();

		switch (direction) 
		{
		case ItemDirection.North:
			this.transform.Rotate(0,90,0);
			direction = ItemDirection.East;
			break;
		case ItemDirection.East:
			this.transform.Rotate(0,90,0);
			direction = ItemDirection.South;
			break;
		case ItemDirection.South:
			this.transform.Rotate(0,90,0);
			direction = ItemDirection.West;
			break;
		case ItemDirection.West:
			this.transform.Rotate(0,90,0);
			direction= ItemDirection.North;
			break;
		}
	}

	public void ItemSwitch()
	{
		int temp;

		temp = heightInCells;
		heightInCells = widthInCells;
		widthInCells = temp;
	}

	// Adjust the position with respect to rotation
	public void itemPositionAdjustment(Vector3 pointed)
	{
		// If way is north
		if(direction == ItemDirection.North)
		{
			transform.position =  new Vector3(pointed.x + widthInCells , pointed.y, pointed.z + heightInCells);
		}
		// If way is east
		else if(direction == ItemDirection.East)
		{
			transform.position =  new Vector3(pointed.x + widthInCells, pointed.y, pointed.z);
		}
		// If way is south
		else if(direction == ItemDirection.South)
		{
			transform.position =  new Vector3(pointed.x, pointed.y, pointed.z);
		}
		// If way is west
		else if(direction == ItemDirection.West)
		{
			transform.position =  new Vector3(pointed.x, pointed.y, pointed.z + heightInCells);
		}
	}

	public bool IsValidPosition(Vector3 lb, int roomID, Room room)
	{
		///switch on itemType
		/// might want to move the along walls to make them all follow same route
		switch (type) 
		{
			case ItemType.Door:
			if(IsValidDoorPos(lb, roomID, room))
			{
				return true;
			}
			break;

		case ItemType.Bench:
			if(IsValidBenchPos(lb))
			{
				return true;
			}

			break;
		}

		return false;

	}

	public bool IsValidBenchPos (Vector3 lb)
	{
		//TODO SET THE BENCH VALID
		if (Maps.IsOutSideRoomAlongWall (lb, widthInCells, heightInCells, direction)) 
		{
			Debug.Log("bench correct");
			return true;
		}

		Debug.Log("bench Incorrect");

		return false;
	}

	public bool IsValidDoorPos(Vector3 lb, int roomID, Room room)
	{
		Room checkRoom = room;

		// is both sections inside room ?
		if (Maps.OnFloorMap (lb, widthInCells, heightInCells, roomID)) 
		{
			///check if on wall pos

			if(checkRoom.AgainstWall(direction, this.transform.position,widthInCells, heightInCells))
			{
				mat1.GetComponent<Renderer>().material.color = Color.green;
				mat2.GetComponent<Renderer>().material.color = Color.green;

				return true;
			}

			mat1.GetComponent<Renderer>().material.color = Color.red;
			mat2.GetComponent<Renderer>().material.color = Color.red;


			return false;
		}

		mat1.GetComponent<Renderer>().material.color = Color.red;
		mat2.GetComponent<Renderer>().material.color = Color.red;

		return false;
	}

	public void BuildItem(Vector3 lb, Room room)
	{

		Maps.setFloorMapBlock (lb, widthInCells, heightInCells, 3);
		AssignedRoom = room;

		if (type == ItemType.Door) 
		{
			AssignedRoom.AddDoorToList (this.gameObject);
			//turn off the walls

			room.DisableWallsForDoors (direction, this.transform.position, widthInCells, heightInCells);

			//turn off mats
			mat1.SetActive(false);
			mat2.SetActive(false);
		}
	}
}
