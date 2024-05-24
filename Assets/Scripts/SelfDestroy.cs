using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float TimeBeforeDestroy = 2f;
    void Start()
    {
        StartCoroutine(SelfDestructon());
    }

    // Update is called once per frame
    private IEnumerator SelfDestructon()
    {
        yield return new WaitForSeconds(TimeBeforeDestroy);

        Destroy(gameObject);
    }
}
