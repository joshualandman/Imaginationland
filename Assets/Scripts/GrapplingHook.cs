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

	private float speedY = 0;
	private float speedX = 0;

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

			if(timer < 30)
			{
				pos.x += speedX;
				pos.y += speedY;

			}
			if(timer >= 30.0)
			{
				//hook gets caught on return, we may neeed to turn off collision on the return
				pos.x -= speedX;
				pos.y -= speedY;
			}
			if(timer == 66) // After a long period of time, if the hand is still in motion make it go back to the player to be picked up (hand got stuck)
			{
				pos.x = transform.position.x;
				pos.y = transform.position.y;
			}
			hook.transform.position = pos; //set hook equal to teporary
		}
	}

	void LaunchHook()
	{
		if(Input.GetKeyUp(KeyCode.Mouse1))//when the player presses G
		{
			
			isHookFired = true; // hook is being fired

			//Calculate the speeds that should be used to reach target
			var x = Input.mousePosition.x;
			var y = Input.mousePosition.y;
			Vector3 newVector = Camera.main.ScreenToWorldPoint(new Vector3 (x, y, 1));

			float xdiff = newVector.x - player.transform.position.x;
			float ydiff = newVector.y - player.transform.position.y;

			speedX = xdiff/30.0f;
			speedY = ydiff/30.0f;

			var xHand = transform.position.x;
			var yHand = transform.position.y;

			if(speedX > 0.05) //if they click to the right of the player then make the hand appear to the right
			{
				xHand += .9f;
			}
			else if( speedX < -0.05)//if they click to the left of the player then make the hand appear to the left
			{
				xHand -= .9f;
			}

			if( speedY > 0.05) //if they click above the player then make the hand appear above
			{
				yHand += 1.2f;
			}
			else if( speedY < -0.05)//if they click below the player then make the hand appear below
			{
				yHand -= 1.8f;
			}

			//make the hand appear at the appropriate place around the player
			hook.transform.position = new Vector3(xHand, yHand,-1);

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
