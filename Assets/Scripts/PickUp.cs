using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public CharacterController player;

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name == "Pillow" || other.gameObject.name == "Pillow2(Clone)" )
		{
			Destroy(other.gameObject);
			player.numberOfPillows++;
		}
	}
}
