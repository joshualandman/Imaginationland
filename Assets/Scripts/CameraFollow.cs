using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform followObject;

	// Update is called once per frame
	void Update () {

		Vector3 pos = new Vector3
			(followObject.transform.position.x,
			followObject.transform.position.y -0.1f,
			 transform.position.z);

		transform.position = pos;
	}
}
