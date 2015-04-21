using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {

	float speed;
	private float movementX;
	private float movementY;
	
	// Update is called once per frame
	void Update () {

		// Adjust the speed with respect to zoom and shift key press
		if(Input.GetKey(KeyCode.LeftShift))
		{
			speed = 0.030f;
		}
		else
		{
			speed = 0.015f;
		}
		speed = speed * Camera.main.orthographicSize;
		
		// Get input from keyboard
		movementX = speed*Input.GetAxis("Horizontal");
		movementY = speed*Input.GetAxis("Vertical");
		
		// Move the camera
		transform.Translate(movementX + movementY, 0, movementY - movementX);

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Camera.main.orthographicSize = Camera.main.orthographicSize - 1;
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Camera.main.orthographicSize = Camera.main.orthographicSize + 1;
		}

	}
}
