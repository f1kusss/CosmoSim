using UnityEngine;
using System.Collections;

public class ObjectMoveAndRotate : MonoBehaviour {

	public Transform target; 
	public float _speedRotation = 10f; 
	public float speedMoving = 0.5f;

	private IEnumerator coroutineMoveSun;
	private WaitForSeconds waitForMoveSun;

	[SerializeField] private float waitForMoveSunDelay = 0.1f;
	
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
		RotateAround();

	}

	private void RotateAround()
	{
        transform.RotateAround(target.transform.position, target.transform.up, _speedRotation * Time.deltaTime);
    }
	private void MoveSun()
	{
        if (gameObject.name == "Sun")
        {
            transform.Translate(Vector3.forward * speedMoving * Time.deltaTime);
        }
    }

	[ContextMenu("StartCoroutineMoveSun")]
	public void StartCoroutineMoveSun()
	{
		coroutineMoveSun = MoveSunCor();
		StartCoroutine(coroutineMoveSun);
	}
    [ContextMenu("StopCoroutineMoveSun")]
    public void StopCoroutineMoveSun()
    {
        StopCoroutine(coroutineMoveSun);
    }

	private IEnumerator MoveSunCor()
	{
		while(true)
		{
			yield return waitForMoveSun;
			MoveSun();
		}
	}
}
