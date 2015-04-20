using UnityEngine;
using System.Collections;
using Prime31.StateKit;


// this will be the waiting class for input
public class HospitalIdle : SKState<HospitalStates>{

	#region implemented abstract members of SKState

	public override void begin ()
	{
		_context.Hover.gameObject.SetActive (false);
	}

	public override void update (float deltaTime)
	{
//		Ray ray;
//		RaycastHit hit;
//		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		
//		if (Physics.Raycast (ray, out hit, 100.0f)) 
//		{
//			_context.Hover.position = _context.getTilePoints (hit.point);
//		}
	}

	#endregion



}
