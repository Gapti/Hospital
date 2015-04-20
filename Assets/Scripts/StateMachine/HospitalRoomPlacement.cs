using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class HospitalRoomPlacement :  SKState<HospitalStates>{

	private Vector3 firstClickPos;
	private int xDist;
	private int zDist;
	private float xD = 0;
	private float zD = 0;

	private Vector3 TopLeft;
	private Vector3 TopRight;
	private Vector3 BottomRight;
	private Vector3 BottomLeft;

	private bool SetFirstPoint = false;
	private bool isValid;

	public override void begin ()
	{
		_context.Hover.gameObject.SetActive (true);
	}

	public override void end ()
	{
		SetFirstPoint = false;
	}


	#region implemented abstract members of SKState
	public override void update (float deltaTime)
	{
		Vector3 MouseDragPoint;
		Vector3 ClickDownPoint;

		if (Physics.Raycast (_context.ray, out _context.hit, 100.0f)) {
			if (!SetFirstPoint) {
				_context.Hover.position = _context.getTilePoints (_context.hit.point);
			}

			///first left click store first point
			if (Input.GetMouseButtonDown (0)) {
				SetFirstPoint = true;

				_context.Hover.position = _context.getTilePoints (_context.hit.point);
				_context.Hover.localScale = new Vector3(1,1,1);
				///store click point
				ClickDownPoint = _context.getTilePoints (_context.hit.point);
				firstClickPos = ClickDownPoint;

			}

			if (Input.GetMouseButton (0)) {
				///dragging mouse
				//store dis x and z


				MouseDragPoint = _context.getTilePoints (_context.hit.point);

				xDist = (int)(firstClickPos.x - MouseDragPoint.x);
				zDist = (int)(firstClickPos.z - MouseDragPoint.z);

//				Debug.Log (firstClickPos.x + " topleft " + (firstClickPos.x - xDist) + " TopRight ");
//				Debug.Log (firstClickPos.z + " bottomleft " + (firstClickPos.z - zDist) + " BottomRight ");

				if (firstClickPos.x <= firstClickPos.x - xDist && firstClickPos.z >= firstClickPos.z - zDist) {
					//Debug.Log ("from left to the right, up to down");
					
					TopLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z + 1);
					TopRight = new Vector3 (firstClickPos.x - xDist + 1, firstClickPos.y, firstClickPos.z + 1);
					BottomLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z - zDist);
					BottomRight = new Vector3 (firstClickPos.x - xDist + 1, firstClickPos.y, firstClickPos.z - zDist);
					
				} else if (firstClickPos.x > firstClickPos.x - xDist && firstClickPos.z > firstClickPos.z - zDist) {
					//Debug.Log ("from right to left up to down");
					
					TopRight = new Vector3 (firstClickPos.x + 1, firstClickPos.y, firstClickPos.z + 1);
					TopLeft = new Vector3 (firstClickPos.x - xDist, firstClickPos.y, firstClickPos.z + 1);
					BottomRight = new Vector3 (firstClickPos.x + 1, firstClickPos.y, firstClickPos.z - zDist);
					BottomLeft = new Vector3 (firstClickPos.x - xDist, firstClickPos.y, firstClickPos.z - zDist);
					
				} else if (firstClickPos.x < firstClickPos.x - xDist && firstClickPos.z < firstClickPos.z - zDist) {
					//Debug.Log ("form left to right down to up");
					
					BottomLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z);
					BottomRight = new Vector3 (firstClickPos.x - xDist + 1, firstClickPos.y, firstClickPos.z);
					TopLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z - zDist + 1);
					TopRight = new Vector3 (firstClickPos.x - xDist + 1, firstClickPos.y, firstClickPos.z - zDist + 1);
					
				} else if (firstClickPos.x >= firstClickPos.x - xDist && firstClickPos.z <= firstClickPos.z - zDist) {
					//Debug.Log ("form right to left down to up");
					
					BottomRight = new Vector3 (firstClickPos.x + 1, firstClickPos.y, firstClickPos.z);
					BottomLeft = new Vector3 (firstClickPos.x - xDist, firstClickPos.y, firstClickPos.z);
					TopRight = new Vector3 (firstClickPos.x + 1, firstClickPos.y, firstClickPos.z - zDist + 1);
					TopLeft = new Vector3 (firstClickPos.x - xDist, firstClickPos.y, firstClickPos.z - zDist + 1);
				}
				else
				{
					Debug.Log("cannot detect");
					BottomRight = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z);
					BottomLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z);
					TopRight = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z);
					TopLeft = new Vector3 (firstClickPos.x, firstClickPos.y, firstClickPos.z);
				}

				xD = Mathf.Abs ((float)xDist) + 1;
				zD = Mathf.Abs ((float)zDist) + 1;

				_context.Hover.position = new Vector3 (BottomLeft.x, 0, BottomRight.z); 
				_context.Hover.localScale = new Vector3 (xD, 1, zD);

				//check if we can place

				if (isBlockSumZero (BottomLeft, xD, zD)) {
					_context.HoverRender.material.color = Color.green;
					isValid = true;
				} else {
					_context.HoverRender.material.color = Color.red;
					isValid = false;
				}

			}

			if (Input.GetMouseButtonUp (0)) {
				if (SetFirstPoint && isValid) {
				
					///buid the walls
					Room room = new Room (RoomType.General, BottomLeft, TopLeft, BottomRight, TopRight, xD, zD, _context.HorizontalWall, _context.VerticalWall);
					_context.Rooms.Add (room);

					Maps.setRoomMapBlock (BottomLeft, xD, zD, room.RoomID);
					Maps.setFloorMapBlock (BottomLeft, xD, zD, 2);

					_context.Hover.localScale = (new Vector3 (1, 1, 1));
					SetFirstPoint = false;
					isValid = false;

					_machine.changeState<HospitalIdle>();

				} else {
					//reset

					_context.Hover.localScale = (new Vector3 (1, 1, 1));
					SetFirstPoint = false;
					isValid = false;
				}
			}

			if (Input.GetKey (KeyCode.Escape)) {
				_machine.changeState<HospitalIdle> ();
			}
		}
	}
	#endregion

	bool isBlockSumZero(Vector3 leftBottom, float xRange, float zRange)
	{
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
	
}
