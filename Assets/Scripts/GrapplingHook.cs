using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	public GameObject hook;
	private bool isHookFired = false;
	public GameObject player;
	public LineRenderer rope;

	private Vector3 hookStart = new Vector3(0,-20,0);
	private double timer = 0.0;

	void Start()
	{
		rope.enabled = false;
	}

	void Update()
	{
		LaunchHook();

		Vector3 pos = hook.transform.position;

		if(isHookFired)
		{
			timer++;
			rope.enabled = true;

			if(GetComponent<CharacterController>().moveRight)
			{
				pos.x += 0.3f;
			}
			else
			{
				pos.x -= 0.3f;
			}

			hook.transform.position = pos;

			if(timer >= 30.0)
			{
				//time limit
				//rope.enabled=false;
				//hook.transform.position = hookStart;

				//keep hook out
				hook.transform.position = hook.transform.position;
				timer = 0;
				isHookFired = false;
			}
		}
		rope.SetPosition(0, player.transform.position);
		rope.SetPosition(1, hook.transform.position);

		if(Input.GetKeyUp(KeyCode.T))
		{
			hook.transform.position = hookStart;
			rope.enabled = false;
			isHookFired = false;
		}
	}

	void LaunchHook()
	{
		if(Input.GetKeyUp(KeyCode.G))
		{
			if(GetComponent<CharacterController>().moveRight)
			{
				hook.transform.position = new Vector3(transform.position.x+1,transform.position.y,transform.position.z);
			}
			else
			{
				hook.transform.position = new Vector3(transform.position.x-1,transform.position.y,transform.position.z);
			}


			//Instantiate(rope, hook.transform.position, new Quaternion(0,0,0,0));
			//Instantiate(hook, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0,0,0,0));
			isHookFired = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{ 
		if(other.gameObject.tag == "Grounded")
		{
			//hook.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
		}
	}
}
