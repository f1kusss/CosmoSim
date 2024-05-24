//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAsteroids : MonoBehaviour
{
    public GameObject AsteroidPrefab;

    public int NumberOfAsteroidsOnAxisX = 10;
    public int NumberOfAsteroidsOnAxisY = 10;
    public int NumberOfAsteroidsOnAxisZ = 10;
    public int GridSpacing = 10;
    private void Start()
    {
        for(int i = 0; i < NumberOfAsteroidsOnAxisX; i++)
        {
            for (int j = 0; j < NumberOfAsteroidsOnAxisY; j++)
            {
                for (int k = 0; k < NumberOfAsteroidsOnAxisZ; k++)
                {
                    InstantiateAsteroids(i, j, k);
                }
            }
        }
    }

    private void InstantiateAsteroids(int x, int y, int z)
    {
        Instantiate(AsteroidPrefab, new Vector3(
            transform.position.x + x * GridSpacing + OffSetAsteroid(),
            transform.position.y + y * GridSpacing + OffSetAsteroid(),
            transform.position.z + z * GridSpacing + OffSetAsteroid()), Quaternion.identity, transform);
    }

    private float OffSetAsteroid()
    {
        return Random.Range(-GridSpacing / 3f, GridSpacing / 3f);
    }
}
