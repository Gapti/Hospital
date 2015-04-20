using UnityEngine;
using System.Collections;
using Prime31.StateKit;
using System.Collections.Generic;

public class HospitalStates : MonoBehaviour {

	public float tileWidth;
	public float tileHeight;

	public Ray ray;
	public RaycastHit hit;

	public Transform Hover;
	public GameObject cube;
	public GameObject HorizontalWall;
	public GameObject VerticalWall;
	public Renderer HoverRender;

	public List<Room> Rooms = new List<Room> ();



	public SKStateMachine<HospitalStates> _machine;


	// Use this for initialization
	void Start () {
	
		_machine = new SKStateMachine<HospitalStates> (this, new HospitalIdle());
		_machine.addState (new HospitalRoomPlacement());
		_machine.addState (new HospitalRemoveRoom ());

	}
	
	// Update is called once per frame
	void Update () {
		// update the state machine
		_machine.update( Time.deltaTime );
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Input.GetKey (KeyCode.Escape)) {
			_machine.changeState<HospitalIdle> ();
		}
	}
	

	// Convert world space floor points to tile points
	public Vector3 getTilePoints(Vector3 floorPoints)
	{
		Vector3 tilePoints = new Vector3();
		
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / tileWidth);
		tilePoints.z = (int)(floorPoints.z / tileHeight);
		tilePoints.y = 0;
		
		// Return the tile points
		return tilePoints;
	}
}
