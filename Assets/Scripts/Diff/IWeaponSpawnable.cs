using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public interface IWeaponSpawnable
    {
        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);
    }
}