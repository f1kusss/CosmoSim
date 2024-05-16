using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 0.5f;

	public Vector3 Thrust()
	{
		return Vector3.forward * moveSpeed * Time.deltaTime;
	}
}
