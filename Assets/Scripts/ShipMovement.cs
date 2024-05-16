using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

	public Transform Ship;
	[SerializeField] private List<Engine> Engines = new List<Engine>();

    private void Update()
    {
        Turn();
        Move();
    }

    private void Turn()
    {
		float pitch = rotationSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
		float yaw = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		float roll = rotationSpeed * Input.GetAxis("Roll") * Time.deltaTime;
		Ship.Rotate(pitch, yaw, roll);
    }

    private void Move()
    {
        Vector3 resThrust = new Vector3();
        foreach (var engine in Engines)
        {
            resThrust += engine.Thrust();
        }

        Ship.Translate(resThrust);
    }
}
