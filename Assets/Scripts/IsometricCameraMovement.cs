using UnityEngine;
using System.Collections;

// Isometric camera movement class
public class IsometricCameraMovement : MonoBehaviour 
{
	// Private variables
	private float movementX;
	private float movementY;
	public GameObject IsoCamera;
	private float speed;
	
	// Use this for initialization
	void Start () 
	{
		IsoCamera = GameObject.Find("Camera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Adjust the speed with respect to zoom and shift key press
		if(Input.GetKey(KeyCode.LeftShift))
		{
			speed = 0.030f;
		}
		else
		{
			speed = 0.015f;
		}
		speed = speed * IsoCamera.GetComponent<Camera>().orthographicSize;
		
		// Get input from keyboard
		movementX = speed*Input.GetAxis("Horizontal");
		movementY = speed*Input.GetAxis("Vertical");
		
		// Move the camera
		transform.Translate(movementX + movementY, 0, movementY - movementX);
		
		// Zoom in camera
		if(Input.GetKey(KeyCode.E) && IsoCamera.GetComponent<Camera>().orthographicSize > 3)
        {
        	IsoCamera.GetComponent<Camera>().orthographicSize = IsoCamera.GetComponent<Camera>().orthographicSize - 1;
        }
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && IsoCamera.GetComponent<Camera>().orthographicSize > 3)
        {
        	IsoCamera.GetComponent<Camera>().orthographicSize = IsoCamera.GetComponent<Camera>().orthographicSize - 1;
        }
		
		// Zoom out camera
		if(Input.GetKey(KeyCode.Q) && IsoCamera.GetComponent<Camera>().orthographicSize < 50)
        {
        	IsoCamera.GetComponent<Camera>().orthographicSize = IsoCamera.GetComponent<Camera>().orthographicSize + 1;
        }
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && IsoCamera.GetComponent<Camera>().orthographicSize < 50)
        {
        	IsoCamera.GetComponent<Camera>().orthographicSize = IsoCamera.GetComponent<Camera>().orthographicSize + 1;
        }
	}
}
