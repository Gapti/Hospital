using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {


	public void BuildGPOffice()
	{
		HospitalStates hs = GetComponent<HospitalStates> ();
		hs.roomSelectedFromUI = RoomType.GPOffice;
		hs._machine.changeState<HospitalRoomPlacement> ();
	}

	public void RemoveARoom()
	{
		HospitalStates hs = GetComponent<HospitalStates> ();
		hs._machine.changeState<HospitalRemoveRoom> ();
	}

	public void FurnishRoomDoors()
	{
		HospitalStates hs = GetComponent<HospitalStates>();
		hs.ChosenItem = hs.Door;
		hs._machine.changeState<HostpitalFurnishSelectPos>();
	}

	public void BuildABench()
	{
		HospitalStates hs = GetComponent<HospitalStates>();
		hs.ChosenItem = hs.Bench;
		hs._machine.changeState<HostpitalFurnishSelectPos>();
	}
}
