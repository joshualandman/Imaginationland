using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	//float for jump height
	public float jumpHeight = 10f;

	//insert obejcts
	public GameObject malleableFloor; //floor that will be malleable
	public GameObject grappleText; //text
	public GameObject winText; //text
	public GameObject oneWay; //texture that will be made oneway

	//bools
	public bool isGrounded = false; // is player on the ground
	public bool hasCape = false; // does the player have the cape
	public bool collisionOn = true; //is collision on
	//private bool hasFloor = false; // does the player have the floor tile
	public bool moveRight = true; //is the player moving right?


	public Vector3 start = new Vector3(); // the start position of the palyer for resetting the position
	public int numberOfPillows = 0; // number of pillows the player has
	public string power = ""; //ignore for now (supposed to be used to determine what power up the player has)
	EdgeCollider2D col; //collider for one way
	private Animator anim; // animator that will run animations

	//The line to project the placement of blocks
	public LineRenderer projLine;

	// Use this for initialization
	void Start () {
		//get the collider
		col = oneWay.GetComponent<EdgeCollider2D>();

		// Get the Animator component from the gameObject
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//player presses D
		if (Input.GetKey (KeyCode.D)) {
			//set a temporary position slightly in front of the player
			Vector3 temp = transform.position;
			temp.x += 0.1f;

			//move the player to that forward position
			transform.position = temp;
			moveRight = true; //player is facing right
		}

		//player presses A
		if(Input.GetKey(KeyCode.A))
		{
			//set a position slightly behind the player
			Vector3 temp = transform.position;
			temp.x -= 0.1f;

			//move the player to that backwards position
			transform.position = temp;
			moveRight = false; // player is not facing right
		}

		//player presses W
		if(Input.GetKeyDown(KeyCode.W))
		{
			//if the player is on the ground
			if(isGrounded == true)
			{
				//if the player has a cape
				if(hasCape)
				{
					//jump + cape bonus
					rigidbody2D.AddForce(Vector2.up * (jumpHeight+250));
					isGrounded = false;//no longer on the ground
				}
				//if the player does not have a cape, do normal jump
				else
				{
					rigidbody2D.AddForce(Vector2.up * jumpHeight);
					isGrounded = false;
				}
			}
		}

		//if the player is below the oneway paltform
		if((transform.position.y-.5) < oneWay.transform.position.y)
		{
			//turn off collider
			col.enabled = false;
		}
		//if the player is above the platform, turn collision on
		else
		{
			col.enabled = true;
		}

		CheckAnimationKeys ();
		ProjectPlaceBlock ();
		PlaceBlock();
		Win();
		Escape ();
	}

	//Check the input keys to animate properly
	void CheckAnimationKeys()
	{
		//The D key walks to the right
		if(Input.GetKeyDown(KeyCode.D))
		{
			anim.SetBool("isWalkingRight", true);
		}
		if(Input.GetKeyUp (KeyCode.D))
		{
			anim.SetBool ("isWalkingRight", false);
		}

		//The A key walks to the left
		if(Input.GetKeyDown(KeyCode.A))
		{
			anim.SetBool("isWalkingLeft", true);
		}
		if(Input.GetKeyUp (KeyCode.A))
		{
			anim.SetBool ("isWalkingLeft", false);
		}
	}

	//Display win text
	void Win()
	{
		if ((transform.position.y - .35) >= oneWay.transform.position.y)
		{
			winText.transform.position = new Vector3(0,6.5f,0);
		}
	}

	//Handles exiting the game
	void Escape()
	{
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	//If the player releases the Space key then place a block
	void PlaceBlock ()
	{
		if(numberOfPillows > 0 && Input.GetKeyUp(KeyCode.Space))
		{
			if(moveRight)
			{
				Instantiate(malleableFloor, new Vector3(transform.position.x + 2, transform.position.y + .3f, transform.position.z), new Quaternion(0,0,0,0));
			}
			else
			{
				Instantiate(malleableFloor, new Vector3(transform.position.x - 2, transform.position.y + .3f, transform.position.z), new Quaternion(0,0,0,0));
			}

			numberOfPillows--;

			projLine.enabled = false;
		}
	}

	//If the player holds the space key then a line will be shown of the projected path that the block will take to be placed
	void ProjectPlaceBlock()
	{
		if (numberOfPillows > 0 && Input.GetKey(KeyCode.Space))
		{
			Vector3 mp;
			projLine.SetPosition(0, transform.position);
			if(moveRight)
			{
				mp = new Vector3(transform.position.x + 2, transform.position.y + .3f, transform.position.z);
			}
			else
			{
				mp = new Vector3(transform.position.x - 2, transform.position.y + .3f, transform.position.z);
			}


			mp.z = 0;
			projLine.SetPosition(1, mp);
			projLine.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//if the player is on the ground make it true
		if(other.gameObject.tag == "Grounded" || other.gameObject.name == "Pillow2(Clone)" )
		{
			isGrounded = true;
		}
		//if the player touches lava, reset the level
		if(other.gameObject.tag == "lava")
		{
			Application.LoadLevel("Game");
		}
		//if the player touches the cape, give it to him and display the power in the GUI
		if(other.gameObject.tag == "Cape")
		{
			hasCape = true;
			Destroy(other.gameObject);
			power = "CAPE";
		}
		//if player touches floor, give it to him and display it in the GUI
		if(other.gameObject.name == "Pillow")
		{
			numberOfPillows++;
			Destroy(other.gameObject);
		}
		//If the player touches the hook image at the end of the level then destroy it and allow them to use the Grapple Hook script
		if (other.gameObject.name == "HandImage")
		{
			Instantiate(grappleText, new Vector3(22, -1.7f, 0), new Quaternion(0,0,0,0));
			Destroy(other.gameObject);
			GetComponent<GrapplingHook>().enabled = true;
		}
	}
}
