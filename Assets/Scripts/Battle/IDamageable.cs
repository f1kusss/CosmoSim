using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public interface IDamageable
    {
        float Health { get; }
        void TakeDamage(float damage, Vector3 hitPosition, GameAgent sender);

        void GetHeal(float Heal, Vector3 hitPosition, GameAgent sender);
    }
}