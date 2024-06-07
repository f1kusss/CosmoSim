using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    [CreateAssetMenu(fileName = "DefProjectileDefault", menuName = "Definitions/Battle/ProjectileDefinition")]
    public class ProjectileDefinition : FlywaightDefinition
    {
        // [SerializeField] public float Velocity = 300f;
        [SerializeField] public ProjectileVelocityDefinition ProjectileVelocityDefinition;
        [SerializeField] public float Damage = 100f;

        [SerializeField] public float LifetimeTotal = 10f; // Time before we destroy it (so it won't impact performance)
        [SerializeField] public float DelayAfterHit = 0.1f;
        [SerializeField] public LayerMask LayerMask;
        [SerializeField] public float RaycastDistance = 1.5f; // Raycast advance multiplier 
        [Header("Links")] [SerializeField] public GameObject ImpactPrefab;


        public override Flyweight Create()
        {
            var gameObject = Instantiate(DeffinitionPref);
            gameObject.SetActive(false);
            gameObject.name = DeffinitionPref.name;
            var flyweight = gameObject.AddComponent<Projectile>();
            flyweight.Definition = this;
            return flyweight;
        }
    }
}