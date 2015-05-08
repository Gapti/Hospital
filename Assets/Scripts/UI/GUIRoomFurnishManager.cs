using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GUIRoomFurnishManager : MonoBehaviour {

	public GameObject ItemButtonParent;
	public GameObject ButtonPrefab;

	private HospitalStates hospitalStates;

	void OnEnable()
	{
		///get a ref to the hospital states object
		hospitalStates = GameObject.Find("LevelManager").GetComponent<HospitalStates>();

		print ("i am awake, room is " + hospitalStates.SelectedFurnishRoom.type);

		InitButtonsForRoomType(hospitalStates.SelectedFurnishRoom.type);
	}

	
	void InitButtonsForRoomType (RoomType type)
	{
		switch(type)
		{
		case RoomType.GPOffice:
			CreateGPOfficeRoomItems();
			break;
		}
	}

	void CreateGPOfficeRoomItems ()
	{
		int doorsAllowed = 1;

		///create door button
		Button doorButton = Instantiate(ButtonPrefab).GetComponent<Button>();
		doorButton.onClick.AddListener(BuildManager.instance.FurnishRoomDoors);
		doorButton.onClick.AddListener(delegate {DisableButton(doorButton);});
		doorButton.transform.SetParent(ItemButtonParent.transform);
		doorButton.transform.FindChild("ButtonText").GetComponent<Text>().text = "Door";
		doorButton.transform.FindChild("Amount").GetComponent<Text>().text = (doorsAllowed - hospitalStates.SelectedFurnishRoom.Doors.Count).ToString();

		if(hospitalStates.SelectedFurnishRoom.Doors.Count > 0)
		{
			DisableButton(doorButton);
		}


	}

	void DisableButton(Button button)
	{
		button.interactable = false;
	}

	public void DestoryButtons()
	{
		foreach(Transform child in ItemButtonParent.transform)
		{
			Destroy(child.gameObject);
		}
	}
	
}
