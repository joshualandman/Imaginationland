using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	public GameObject hook;
	private bool isHookFired = false;

	void Update()
	{
		LaunchHook();

		Vector3 pos = hook.transform.position;


		if(isHookFired)
		{
			pos.x+= 0.1f;
			hook.transform.position = pos;
		}
	}

	void LaunchHook()
	{
		if(Input.GetKeyUp(KeyCode.G))
		{
			Instantiate(hook, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0,0,0,0));
			isHookFired = true;
		}
	}
}
