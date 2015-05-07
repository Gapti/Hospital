using UnityEngine;
using System.Collections;

public class OtherPanelManager : MonoBehaviour {


	public GameObject RoomFurnishGO;

	public void OpenRoomFurnishWindow()
	{
		RoomFurnishGO.SetActive(true);
	}

	public void CloseRoomFurnishWindow()
	{
		RoomFurnishGO.SetActive(false);
	}
}
