using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class HostpitalFurnishSelectPos : SKState<HospitalStates> {

	private int roomID;
	private Room selectedRoom;
	
	private Item item;


	#region implemented abstract members of SKState

	public override void begin ()
	{
		item = GameObject.Instantiate (_context.ChosenItem).GetComponent<Item>();
		_context.Hover.gameObject.SetActive (false);
		roomID = 0;
	}


	public override void update (float deltaTime)
	{


		///lets put the doors on might need to miove this eventually

		if (Physics.Raycast (_context.ray, out _context.hit, 100.0f)) {

			//move the hover to curser position
			_context.Hover.position = _context.getTilePoints(_context.hit.point);
			item.itemPositionAdjustment(_context.Hover.position);

			///collect roomid
			roomID = Maps.GetRoomID((int)_context.Hover.position.x,(int) _context.Hover.position.z);

			if(Input.GetKeyDown(KeyCode.Tab))
			{
				item.Rotate();
			}

			if(item.IsValidPosition(_context.Hover.position, roomID, _context.GetRoomFromID(roomID)))
			{
				if(Input.GetMouseButton(0))
				{
					item.BuildItem(_context.Hover.position, _context.GetRoomFromID(roomID));

					_machine.changeState<HospitalIdle>();
				}
			}
			else
			{

			}

		}

	}

	#endregion
}
