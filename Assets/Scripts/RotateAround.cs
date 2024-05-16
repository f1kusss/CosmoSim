using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{

	public Transform target; 
	public float speedRotation;
    private float speedMovingSun = 0.5f;

    private IEnumerator coroutine;
	private WaitForSeconds waitForMoveSun;
	[SerializeField]private float waitForMoveSunDelay = 0.1f;

	private void Start() 
	{
		if (target == null) 
		{
			target = this.gameObject.transform;
			Debug.Log ("RotateAround target not specified. Defaulting to parent GameObject");
		}

		waitForMoveSun = new WaitForSeconds(waitForMoveSunDelay);

		StartCoroutineMoveSun();
	}

	private void Update () 
	{
		RotateAroundTheTarget();
	}

	private void RotateAroundTheTarget()
	{
		transform.RotateAround(target.transform.position, target.transform.up, speedRotation * Time.deltaTime);
	}

	private void MoveSun()
	{
		if(gameObject.name == "Sun")
		{
			transform.Translate(Vector3.forward * speedMovingSun * Time.deltaTime);
		}	
	}
	public void StartCoroutineMoveSun()
	{
		coroutine = CoroutineMoveSun();
		StartCoroutine(coroutine);
	}
    public void StopCoroutineMoveSun()
	{
		StopCoroutine(coroutine);
	}

	private IEnumerator CoroutineMoveSun()
	{
		while (true)
		{
			yield return waitForMoveSun;
			MoveSun();
		}
	}
}
