using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public interface IWeapons
    {
        Vector3 FireWeapon(Vector3 targetPosition);

        void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);
        void VisualizeFireWeapon(Vector3 targetPosition);

        //void Damage(float damage, Vector3 targetHitPosition, GameAgent sender);
    }
}