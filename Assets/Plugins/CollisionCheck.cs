using UnityEngine;
using System.Collections;

public class CollisionCheck : MonoBehaviour {

	public bool hit;

	void OnTriggerEnter(Collider collision)
	{
		print (collision.gameObject.name);
		if (collision.gameObject.tag== "Wall") 
		{
			hit = true;
		}
	}

	void OnTriggerExit(Collider collision)
	{
		hit = false;
	}
}
