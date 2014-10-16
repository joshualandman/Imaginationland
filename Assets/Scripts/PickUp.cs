using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public CharacterController player;

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("H");
		if(other.gameObject.name == "Pillow")
		{

			//player.hasFloor = true;
			player.numberOfPillows++;
			Destroy(other.gameObject);
		}
	}
}
