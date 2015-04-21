using UnityEngine;
using System.Collections;

public enum ItemDirection { North, South, East, West }
public enum ItemType { Door }

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
	

	void Start()
	{
		itemID = ++itemIDGenerator;
		direction = ItemDirection.South;

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
		//not a room
		if (roomID == 0)
		{
			return false;
		}

		///switch on itemType
		switch (type) 
		{
			case ItemType.Door:
			if(IsValidDoorPos(lb, roomID, room))
			{
				return true;
			}
			break;
		}

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
				return true;
			}

			return false;
		}

		return false;
	}

	public void BuildItem(Vector3 lb, Room room)
	{

		Maps.setFloorMapBlock (lb, widthInCells, heightInCells, 3);
		AssignedRoom = room;

		//turn off the walls

		room.DisableWallsForDoors (direction, this.transform.position, widthInCells, heightInCells);
	}
}
