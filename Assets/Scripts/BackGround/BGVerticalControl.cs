using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGVerticalControl : MonoBehaviour
{
	public Transform cameraTransform;
	public float offSet;

	void Update()
	{
		// follow camera's y position
		transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offSet, transform.position.z);
	}
}
