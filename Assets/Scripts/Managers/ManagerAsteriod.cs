using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class ManagerAsteriod : MonoBehaviour
    {
        public GameObject AsteroidPref;
        [SerializeField] private int _AsteroidsOnAxisX = 13;
        [SerializeField] private int _AsteroidsOnAxisY = 13;
        [SerializeField] private int _AsteroidsOnAxisZ = 13;
        [SerializeField] private int _spaceBetweenAsteroids = 20;

        void Start()
        {
            for (int i = 0; i < _AsteroidsOnAxisX; i++)
            {
                for (int j = 0; j < _AsteroidsOnAxisY; j++)
                {
                    for (int k = 0; k < _AsteroidsOnAxisZ; k++)
                    {
                        InstantiateAsteroid(Random.Range(i, _spaceBetweenAsteroids),
                            Random.Range(j, _spaceBetweenAsteroids), Random.Range(k, _spaceBetweenAsteroids));
                    }
                }
            }
        }


        private void InstantiateAsteroid(int x, int y, int z)
        {
            Instantiate(AsteroidPref, new Vector3(
                    transform.position.x + x * OffsetAsteroid(),
                    transform.position.y + y * OffsetAsteroid(),
                    transform.position.z + z * OffsetAsteroid()),
                Quaternion.identity, transform);
        }

        private float OffsetAsteroid()
        {
            return Random.Range(-_spaceBetweenAsteroids / 4, _spaceBetweenAsteroids * 4);
        }
    }
}