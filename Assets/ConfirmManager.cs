using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmManager : MonoBehaviour {

	public GameObject ConfirmPanel;
	public Button OKButton;
	public Button NoButton;

	public static ConfirmManager Instance;

	
	void Awake()
	{
		Instance = this;
	}

	public void Choice( UnityAction OKEvent, UnityAction NoEvent )
	{
		ConfirmPanel.SetActive(true);

		OKButton.onClick.RemoveAllListeners();
		OKButton.onClick.AddListener(OKEvent);
		OKButton.onClick.AddListener(CloseWindow);

		NoButton.onClick.RemoveAllListeners();
		NoButton.onClick.AddListener(NoEvent);
		NoButton.onClick.AddListener(CloseWindow);
	}

	void CloseWindow ()
	{
		ConfirmPanel.SetActive(false);
	}
}
