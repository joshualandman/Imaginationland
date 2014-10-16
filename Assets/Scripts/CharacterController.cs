using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float jumpHeight = 10f;
	public GameObject malleableFloor;
	public bool isGrounded = false;
	public bool hasCape = false;
	public Vector3 start = new Vector3();
	public int numberOfPillows = 0;
	public string power = "";
	public bool collisionOn = true;
	public GameObject oneWay;
	EdgeCollider2D col;
	private bool hasFloor = false;
	//The line to project the placement of blocks
	public LineRenderer projLine;
	public bool moveRight = true;
	public GameObject grappleText;
	public GameObject winText;

	// Use this for initialization
	void Start () {
		col = oneWay.GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D))
		{
			Vector3 temp = transform.position;
			temp.x += 0.1f;
			transform.position = temp;
			moveRight = true;
		}
		if(Input.GetKey(KeyCode.A))
		{
			Vector3 temp = transform.position;
			temp.x -= 0.1f;
			transform.position = temp;
			moveRight = false;
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			if(isGrounded == true /*&& hasCape*/)
			{
				if(hasCape)
				{
					rigidbody2D.AddForce(Vector2.up * (jumpHeight+250));
					isGrounded = false;
				}
				else
				{
					rigidbody2D.AddForce(Vector2.up * jumpHeight);
					isGrounded = false;
				}
			}
		}

		if(transform.position.y <= -61)
		{
			transform.position = start;
		}

		if((transform.position.y-.5) < oneWay.transform.position.y)
		{
			col.enabled = false;
		}
		else
		{
			col.enabled = true;
		}

		ProjectPlaceBlock ();
		PlaceBlock();
		Win();
		Escape ();
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

			hasFloor = false;
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
		if(other.gameObject.tag == "Grounded")
		{
			isGrounded = true;
		}
		if(other.gameObject.tag == "lava")
		{
			//transform.position = start;
			Application.LoadLevel("Game");
		}
		if(other.gameObject.tag == "Cape")
		{
			hasCape = true;
			Destroy(other.gameObject);
			power = "CAPE";
		}
		/*if(other.gameObject.name == "Floor")
		{
			hasFloor = true;
			numberOfPillows++;
			Destroy(other.gameObject);
		}*/
		//If the player touches the hook image at the end of the level then destroy it and allow them to use the Grapple Hook script
		if (other.gameObject.name == "HookImage")
		{
			//Debug.Log("HERE");
			Instantiate(grappleText, new Vector3(22, -1.7f, 0), new Quaternion(0,0,0,0));
			Destroy(other.gameObject);
			GetComponent<GrapplingHook>().enabled = true;
		}
	}
}
