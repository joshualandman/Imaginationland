using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {

	//insert player to use
	public CharacterController player;

	void OnGUI()
	{
		GUI.Box ( new Rect (10, 20, 200, 90), "Player:"); // display player
		GUI.Label ( new Rect (20, 50, 200, 20), "Number of Pillows: " + player.numberOfPillows); // display number of pillows
		GUI.Label ( new Rect (20, 80, 200, 20), "Power: " + player.power); // display power
	}
}
