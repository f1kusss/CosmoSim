using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class ShipHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health;

        public float Health => _health;

        public void TakeDamage(float damage, Vector3 hitPosition, GameAgent sender)
        {
            _health -= damage;
            Debug.Log($"Attaker{sender.gameObject.name}");
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void GetHeal(float Heal, Vector3 hitPosition, GameAgent sender)
        {
            throw new System.NotImplementedException();
        }
    }
}