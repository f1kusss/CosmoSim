using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class AsteroidHealth : MonoBehaviour, IDamageable
    {
        public float Health { get; set; }
        [SerializeField] private GameObject _explodePref;
        [SerializeField] private GameObject _asteroidPref;
        public bool destroyed = false;
        public int _divisionCounter;

        public void TakeDamage(float damage, Vector3 hitPosition, GameAgent sender)
        {
            Health -= damage;
            if (Health <= 0 && !destroyed)
            {
                if (_explodePref)
                    Instantiate(_explodePref, transform.position, Quaternion.identity);
                if (_divisionCounter > 0)
                {
                    Vector3 asteroid1Pos = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + Random.Range(-1f, 1f),
                        transform.position.z + Random.Range(-1f, 1f));
                    Vector3 asteroid2Pos = new Vector3(transform.position.x + Random.Range(-1f, 1f),
                        transform.position.y + Random.Range(-1f, 1f),
                        transform.position.z + Random.Range(-1f, 1f));
                    var a1 = Instantiate(_asteroidPref,
                        asteroid1Pos + _asteroidPref.transform.localScale, Quaternion.identity);
                    var a2 = Instantiate(_asteroidPref,
                        asteroid2Pos - _asteroidPref.transform.localScale, Quaternion.identity);
                    a1.GetComponent<AsteroidHealth>()._divisionCounter = --_divisionCounter;
                    a2.GetComponent<AsteroidHealth>()._divisionCounter = --_divisionCounter;
                }

                ManagerScore.Instance.AddScore(1);
                destroyed = true;
                Destroy(gameObject);
            }
        }

        public void GetHeal(float Heal, Vector3 hitPosition, GameAgent sender)
        {
            throw new System.NotImplementedException();
        }
    }
}