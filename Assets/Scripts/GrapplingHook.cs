using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	//insert objects
	public GameObject player; // player to manipulate
	public GameObject hook; //what object we're using for the hook

	private bool isHookFired = false; //has the hook been fired
	public LineRenderer rope; // rope 
	private Vector3 hookStart = new Vector3(0,-20,0); // position off screen for the hook
	private double timer = 0.0; // timer for the hook to be out

	void Start()
	{
		//turn the rope off at the beginning
		rope.enabled = false;
	}

	void Update()
	{
		LaunchHook(); //test ffor input and do actions base on Input

		Vector3 pos = hook.transform.position; // set placeholder to start position


		if(isHookFired) //if a hook is fired
		{
			timer++; //increment timer
			rope.enabled = true; //turn rope on
			rope.SetPosition(0, player.transform.position); // connect rope to player
			rope.SetPosition(1, hook.transform.position); // connect rope to the hook

			//if the player is moving right
			if(GetComponent<CharacterController>().moveRight)
			{
				pos.x += 0.3f; //have hook shoot in front of player
			}
			else
			{
				pos.x -= 0.3f; //have hook shoot behind the player
			}

			hook.transform.position = pos; //set hook equal to teporary
			hook.transform.position = hook.transform.position;

			if(timer >= 30.0)
			{
				//hook gets caughter on return, we may neeed to turn off collision on the return

				if(hook.transform.position.x > player.transform.position.x) // if hook is in front of the player, come towards player
				{
					pos.x -= 0.5f;
				}
				else if(hook.transform.position.x < player.transform.position.x) // if hook is behind the player, come towards the player
				{
					pos.x += 0.5f;
				}
				
				hook.transform.position = pos; //set hook equal to teporary
			}
		}
	}

	void LaunchHook()
	{
		if(Input.GetKeyUp(KeyCode.G))//when the player presses G
		{
			if(GetComponent<CharacterController>().moveRight) //if they're facing forward, shoot right
			{
				hook.transform.position = new Vector3(transform.position.x+2,transform.position.y,transform.position.z);
			}
			else//otherwise shoot left
			{
				hook.transform.position = new Vector3(transform.position.x-2,transform.position.y,transform.position.z);
			}

			isHookFired = true; // hook is being fired
		}

		if(Input.GetKeyUp(KeyCode.T))// if the player presses T
		{
			hook.transform.position = hookStart; //reset hook
			rope.enabled = false; //turn off the rope
			isHookFired = false; // hook is not being fired
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{ 
		if(other.gameObject.name == "Hand")
		{
			timer = 0;
			hook.transform.position = hookStart;
			isHookFired = false;
			rope.enabled = false;
		}
	}
}
