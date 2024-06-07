using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cosmosim
{
    public class ShipWeapons : MonoBehaviour
    {
        public SpaceShip spaceShip;
        public Rigidbody ShipRigidbody;
        public List<IWeapons> weaponsList = new List<IWeapons>();
        public float _fireDistance = 250f;

        [ContextMenu("InitWeapons")]
        public void InitWeapons()
        {
            weaponsList = GetComponentsInChildren<IWeapons>().ToList();
            foreach (var weapon in weaponsList)
            {
                weapon.Initialize(new DataWeaponExtrinsic()
                    { ShipRigidbody = ShipRigidbody, GameAgent = spaceShip.shipAgent });
            }
        }

        public void Awake()
        {
            InitWeapons();
            if (spaceShip == null)
                spaceShip = GetComponentInParent<SpaceShip>();
            if (ShipRigidbody == null)
                ShipRigidbody = GetComponentInParent<Rigidbody>();
        }


        void OnEnable()
        {
            spaceShip.InputShipWeapons.OnAttacInput += FireWeapons;
        }

        private void OnDisable()
        {
            spaceShip.InputShipWeapons.OnAttacInput -= FireWeapons;
        }

        public void FireWeapons()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, _fireDistance))
            {
                foreach (var weapon in weaponsList)
                {
                    weapon.FireWeapon(hit.point);
                }

                Debug.Log("Fire IF");
            }
            else
            {
                foreach (var weapon in weaponsList)
                {
                    weapon.FireWeapon(ray.origin + ray.direction * _fireDistance);
                }

                Debug.Log("Fire ELSE");
            }
        }
    }
}