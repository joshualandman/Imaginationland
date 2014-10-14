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
		if(GUI.Button( new Rect(Screen.height/2, Screen.width/2, 300, 100), "START"))
		{
			isClicked = true;
		}
	}

}
