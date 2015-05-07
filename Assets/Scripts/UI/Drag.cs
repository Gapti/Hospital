using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IPointerClickHandler{
	#region IDragHandler implementation
	

	public void OnDrag (PointerEventData eventData)
	{
		this.transform.position = Input.mousePosition;
	}


	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{

	}

	#endregion
	#endregion



}
