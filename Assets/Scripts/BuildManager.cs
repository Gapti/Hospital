using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {


	public void BuildRoomGeneral()
	{
		HospitalStates hs = GetComponent<HospitalStates> ();
		hs._machine.changeState<HospitalRoomPlacement> ();
	}

	public void RemoveARoom()
	{
		HospitalStates hs = GetComponent<HospitalStates> ();
		hs._machine.changeState<HospitalRemoveRoom> ();
	}

	public void FurnishRoomSelect()
	{
		HospitalStates hs = GetComponent<HospitalStates>();
		hs._machine.changeState<HostpitalFurnishSelectPos>();
	}
}
