using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    [CreateAssetMenu(fileName = "DefProjectileVelocityDefault",
        menuName = "Definitions/Battle/ProjectileVelocityDefinition")]
    public class ProjectileVelocityDefinition : ScriptableObject
    {
        [SerializeField] public string ID = "";

        [TextArea] [SerializeField] public string Name = "";
        [SerializeField] public float Velocity = 300f;

        private void OnValidate()
        {
            if (ID == "")
            {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}