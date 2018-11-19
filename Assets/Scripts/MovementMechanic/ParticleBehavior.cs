using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {
	public Transform center;
	public Vector3 desiredPosition;
	public float radius = 2.0f;
	public float radiusSpeed = 0.5f;
	public float rotationSpeed = 80.0f;

	void Start()
	{
		transform.position = (transform.position - center.position).normalized * radius + center.position;
	}

	void Update()
	{
        transform.RotateAround(center.position, Vector3.forward, rotationSpeed * Time.deltaTime);
		desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = desiredPosition;
	}
}
