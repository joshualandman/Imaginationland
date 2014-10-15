using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {

	public CharacterController player;
	int bob = 2;
	string power = "CAPE";

	void OnGUI()
	{
		GUI.Box ( new Rect (10, 20, 200, 90), "Player:");
		GUI.Label ( new Rect (20, 50, 200, 20), "Number of Pillows: " + player.numberOfPillows);
		GUI.Label ( new Rect (20, 80, 200, 20), "Power: " + player.power);
	}
}
