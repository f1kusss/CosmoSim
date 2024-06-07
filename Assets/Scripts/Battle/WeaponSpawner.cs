using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class WeaponSpawner : MonoBehaviour, IWeapons
    {
        [SerializeField] private FlywaightDefinition _flywaightDefinition;

        [SerializeField] private WeaponSpawnerDefinition _weaponSpawnerDefinition;

        //ExtansicData
        private DataWeaponExtrinsic _dataWeaponExtrinsic;

        // Serialized for Debug
        [Header("Inner Workings")] [SerializeField]
        private bool _canFire = true;

        // private WaitForSeconds _waitForSecondsSpawning;
        private WaitForSeconds _waitForSecondsMuzzleFlash;
        private float _waitTimeMuzzleFlash = 0.1f;
        private float _waitTimeSpawning => _weaponSpawnerDefinition.ColldownTimeTotal;

        [Header("Links")] [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject _muzzleFlash;


        private void OnEnable()
        {
            _canFire = true;
        }

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)
        {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
            _waitForSecondsMuzzleFlash = new WaitForSeconds(_waitTimeMuzzleFlash);
        }

        public Vector3 FireWeapon(Vector3 targetPosition)
        {
            if (!_canFire)
                return Vector3.zero;


            var spawned = FactoryFlyweight.Instance.Spawn(_flywaightDefinition, transform.position,
                Quaternion.LookRotation(targetPosition - transform.position));
            spawned.GetComponent<IWeaponSpawnable>().Initialize(_dataWeaponExtrinsic);
            VisualizeFireWeapon(targetPosition);

            StartCoroutine(ExecuteCooldown(_waitTimeSpawning));

            return Vector3.zero;
        }


        // _muzzle VFX
        public void VisualizeFireWeapon(Vector3 targetPosition)
        {
            if (_muzzleFlash != null)
            {
                StartCoroutine(VisualizeMuzzleFlash(targetPosition));
            }
        }

        private IEnumerator VisualizeMuzzleFlash(Vector3 targetPosition)
        {
            _muzzleFlash.transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
            _muzzleFlash.SetActive(true);

            yield return _waitForSecondsMuzzleFlash;
            _muzzleFlash.SetActive(false);
        }

        private IEnumerator ExecuteCooldown(float delay)
        {
            _canFire = false;

            yield return new WaitForSeconds(delay);
            _canFire = true;
        }
    }
}