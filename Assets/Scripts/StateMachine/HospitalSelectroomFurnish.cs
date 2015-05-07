using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class HospitalSelectRoomFurnish : SKState<HospitalStates> {

	private int selectedRoomID;
	private Room selectedRoom;

	#region implemented abstract members of SKState

	public override void end ()
	{
		_context.Hover.localScale = new Vector3 (1, 1, 1);

		///open the furnish window
		OtherPanelManager opm = BuildManager.instance.transform.GetComponent<OtherPanelManager>();
		opm.OpenRoomFurnishWindow();
	}

	public override void update (float deltaTime)
	{
		_context.Hover.localScale = new Vector3 (1, 1, 1);
		
		if (Physics.Raycast (_context.ray, out _context.hit, 100.0f)) {
			
			///move curser and detect the room id from hit.point
			
			selectedRoomID = Maps.GetRoomID((int)_context.hit.point.x, (int)_context.hit.point.z);
			
			if( selectedRoomID > 0)
			{
				////show selected room
				
				selectedRoom = _context.GetRoomFromID(selectedRoomID);
				
				_context.Hover.position = _context.getTilePoints( new Vector3(selectedRoom.BottomLeft.x, 0, selectedRoom.BottomRight.z));
				
				_context.Hover.localScale = new Vector3 (selectedRoom.xLength, 1, selectedRoom.zLength);
				
				_context.Hover.gameObject.SetActive (true);
				_context.Hover.SetAsLastSibling();
				
				
			}
			else
			{
				selectedRoom =  null;
				_context.Hover.gameObject.SetActive (false);
			}
			
			if(Input.GetMouseButton(0) && selectedRoom != null)
			{
				_context.SelectedFurnishRoom = selectedRoom;
				_context.SelectedFurnishRoomID = selectedRoomID;

				///change to furnish state
				_machine.changeState<HospitalIdle>();

			}
		}
	}

	#endregion



}
