using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {

	private double count = 0.0;

	void Update()
	{
		count += Time.deltaTime;
		if(count >= 5.0)
		{
			Application.LoadLevel("Game");
		}
	}

}
