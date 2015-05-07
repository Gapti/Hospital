using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void OnEnable()
	{
		instance = this;
	}

	void OnDisable()
	{
		instance = null;
	}

	public void BuildGPOffice()
	{
		GameObject g =GameObject.Find("LevelManager");
		HospitalStates hs = g.GetComponent<HospitalStates> ();
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
		GameObject g =GameObject.Find("LevelManager");
		HospitalStates hs = g.GetComponent<HospitalStates>();
		hs.ChosenItem = hs.Door;
		hs._machine.changeState<HostpitalFurnishSelectPos>();
	}

	public void BuildABench()
	{
		HospitalStates hs = GetComponent<HospitalStates>();
		hs.ChosenItem = hs.Bench;
		hs._machine.changeState<HostpitalFurnishSelectPos>();
	}

	public void SelectRoomToFurnish()
	{
		GameObject g =GameObject.Find("LevelManager");
		HospitalStates hs = g.GetComponent<HospitalStates>();
		hs._machine.changeState<HospitalSelectRoomFurnish>();
	}
}
