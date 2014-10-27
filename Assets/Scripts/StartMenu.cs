using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public bool isClicked = false;

	void Update()
	{
		if(isClicked)
		{
			Application.LoadLevel("Instructions");
			isClicked = false;
		}
	}

	void OnGUI()
	{
		if(GUI.Button( new Rect(Screen.width/2 - 150, Screen.height - 100, 300, 100), "START"))
		{
			isClicked = true;
		}
	}

}
