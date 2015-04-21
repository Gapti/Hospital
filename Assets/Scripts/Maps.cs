using UnityEngine;
using System.Collections;

public class Maps {

	// Maps using vector 3 not vector 2 x,z ignoring the y
	//watch out about rounding up the values with mathf.abs check not - numbers coming from the lenghth and height
	
	/// <summary>
	/// Floor map is used to keep track of map
	/// 0 for empty cell outside room
	/// 1 for non-empty cell outside room
	/// 2 for empty cell inside room
	/// 3 for non-empty cell inside room
	/// 4 for empty cell outside room door
	/// </summary>
	private static int [,] floorMap = new int[100,100];
	
	/// <summary>
	/// Room map is used to keep track of rooms
	/// 0 for no room
	/// 1,2,3...999 Room IDs
	/// </summary>
	private static int [,] roomMap = new int[100,100];

	public static int GetFloorMapValue(Vector3 points)
	{
		return floorMap [(int)points.x, (int)points.z];
	}

	public static int GetFloorMapValue(int xPoint, int zPoint)
	{
		return floorMap [xPoint, zPoint];
	}

	public static int GetRoomID(int xPoint, int zPoint)
	{
		return roomMap [xPoint, zPoint];
	}

	public static bool OnFloorMap(Vector3 leftBottom, int xD, int zD, int val)
	{
		for (int a = 0; a < xD; a++) 
		{
			for( int b = 0; b < zD; b++)
			{
				if(floorMap[(int)leftBottom.x + a, (int)leftBottom.z + b] != 2)
				{
					return false;
				}
			}
		}

		return true;
	}

	public static bool IsOutSideRoomAlongWall(Vector3 pos, int xD, int zD, ItemDirection direction)
	{


		return false;
	}
	

	// this is where stuff is on the map floor
	public static void setFloorMapBlock(Vector3 leftBottom, float xD, float zD, int val)
	{

		xD = Mathf.Abs (xD);
		zD = Mathf.Abs (zD);

		for(int i = 0; i<xD; i++)
		{
			for(int j = 0; j<zD; j++)
			{
				floorMap[((int)leftBottom.x)+i, ((int)leftBottom.z)+j] = val;
			}
		}
	}

	//this is map of rooms
	public static void setRoomMapBlock(Vector3 leftBottom, float xD, float zD, int val)
	{
		xD = Mathf.Abs (xD);
		zD = Mathf.Abs (zD);
		
		for(int i = 0; i<xD; i++)
		{
			for(int j = 0; j<zD; j++)
			{
				roomMap[((int)leftBottom.x)+i, ((int)leftBottom.z)+j] = val;
			}
		}
	}
	
}
