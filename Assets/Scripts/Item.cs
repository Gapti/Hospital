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
		DoorSwitch ();

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

	public void DoorSwitch()
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

	public bool IsValidPosition(Vector3 lb, int roomID)
	{
		//not a room
		if (roomID == 0)
		{
			Debug.Log ("no room");
			return false;
		}

		if (Maps.OnFloorMap (lb, widthInCells, heightInCells, roomID)) 
		{
			return true;
		}

		return false;

	}
}
