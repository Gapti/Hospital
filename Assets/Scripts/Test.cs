using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

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


	void Update()
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
				selectedCell = getTilePoints(hit.point);
				Selecto.transform.position = selectedCell;
				Selecto.SetActive (true);
			
			}

//			if(Input.GetMouseButton(0))
//			{
//
//			
//			///find distance to drag point
//			DraggedPos.position = getTilePoints(hit.point);
//			
//			
//			/// measure distance between these objects
//			xDist = selectedCell.x - DraggedPos.position.x;
//			zDist = selectedCell.z - DraggedPos.position.z;
//			
//			
//			//set the middle point
//			MiddleDragPos = new Vector3(selectedCell.x - xDist / 2,0,selectedCell.z - zDist / 2);
//			
//			///move the Scale pice into pos
//			Selecto.transform.position = MiddleDragPos;
//			Selecto.transform.localScale = new Vector3(xDist,0.1f, zDist);
//			
//			
//			print (selectedCell.x + " topleft " + (selectedCell.x - xDist) + " TopRight ");
//			print (selectedCell.z + " bottomleft " + (selectedCell.z - zDist) + " BottomRight ");
//			
//			
//			///get the correct Position of the corner cubes
//			
//			if (selectedCell.x < selectedCell.x - xDist && selectedCell.z > selectedCell.z - zDist) {
//								print ("from left to the right, up to down");
//				
//				TopLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
//				TopRight = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
//				BottomLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
//				BottomRight = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
//				
//			} else if (selectedCell.x > selectedCell.x - xDist && selectedCell.z > selectedCell.z - zDist) 
//			{
//								print ("from right to left up to down");
//				
//				TopRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
//				TopLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
//				BottomRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
//				BottomLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
//				
//			} else if (selectedCell.x < selectedCell.x - xDist && selectedCell.z < selectedCell.z - zDist)
//			{
//								print ("form left to right down to up");
//				
//				BottomLeft = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
//				BottomRight= new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
//				TopLeft= new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
//				TopRight= new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
//				
//			} else if (selectedCell.x > selectedCell.x - xDist && selectedCell.z < selectedCell.z - zDist)
//			{
//				print ("form right to left down to up");
//				
//				BottomRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z);
//				BottomLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z);
//				TopRight = new Vector3 (selectedCell.x, selectedCell.y, selectedCell.z - zDist);
//				TopLeft = new Vector3 (selectedCell.x - xDist, selectedCell.y, selectedCell.z - zDist);
//			}
//
//
//			}

//			if(Input.GetMouseButtonUp(0))
//			{
//
//				GameObject tl =(GameObject) Instantiate(cubePrefab, TopLeft, Quaternion.identity);
//				GameObject tr =(GameObject) Instantiate(cubePrefab, TopRight, Quaternion.identity);
//				GameObject bl =(GameObject) Instantiate(cubePrefab, BottomLeft, Quaternion.identity);
//				GameObject br = (GameObject)Instantiate(cubePrefab, BottomRight, Quaternion.identity);
//				
//				tl.name = "TL";
//				tr.name = "TR";
//				bl.name = "BL";
//				br.name = "BR";
//
//				selectedCell = new Vector3(0,0,0);
//			}
//			

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
