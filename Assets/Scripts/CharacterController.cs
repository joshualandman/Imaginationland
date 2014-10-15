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
		}
		if(Input.GetKey(KeyCode.A))
		{
			Vector3 temp = transform.position;
			temp.x -= 0.1f;
			transform.position = temp;
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

		PlaceBlock();
	}


	void PlaceBlock ()
	{
		if(numberOfPillows > 0 && Input.GetKeyDown(KeyCode.F))
		{
			Instantiate(malleableFloor, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), new Quaternion(0,0,0,0));
			hasFloor = false;
			numberOfPillows--;
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
		if(other.gameObject.name == "Floor")
		{
			hasFloor = true;
			numberOfPillows++;
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Cape")
		{
			hasCape = true;
			Destroy(other.gameObject);
			power = "CAPE";
		} 
	}
}
