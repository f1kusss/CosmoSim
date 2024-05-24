using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Kosmos6
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour, IWeapon
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private ShipWeapons _ShipWeapons;

        public float damageAmount = 5f;
        public bool canFire;
        public float maxDistance = 100f;

        private Coroutine _coroutineFiring;
        private WaitForSeconds _waitForFiring;
        [SerializeField] private float _waitTimeFiring = 0.1f;

        public List<IDamageable> TargetsHit = new List<IDamageable>();

        private void Awake()
        {
            if(_ShipWeapons == null)
                _ShipWeapons = GetComponentInParent<ShipWeapons>();
            if (_lineRenderer == null)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
        }
        void Start()
        {
            _waitForFiring = new WaitForSeconds(_waitTimeFiring);

            _lineRenderer.enabled = false;
            canFire = true;
        }

        public Vector3 FireWeapon(Vector3 targetPosition)
        {
            RaycastHit hitInfo;
            var direction = targetPosition - transform.position;
            if (Physics.Raycast(transform.position, direction, out hitInfo, maxDistance))
            {
                var targetHit = hitInfo.transform;
                if (targetHit != null)
                {
                    Debug.Log($"FireWeapon. targetHit: {targetHit.name}");
                    var damageableHit = targetHit.GetComponent<IDamageable>();
                    if (damageableHit != null)
                    {
                        TargetsHit.Add(damageableHit);
                        Damage(damageAmount, targetHit.position, _ShipWeapons._Spaceship.ShipAgent);
                    }
                    VisualizeFiring(targetHit.position);

                    return targetHit.position;
                }
                    
            }

            VisualizeFiring(transform.position + direction.normalized * maxDistance);
            //If nothing hited
            return targetPosition;
        }

        public void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender)
        {
            foreach(var targetHit in TargetsHit)
            {
                targetHit.ReceiveDamage(damageAmount, targetHitPosition, sender);
            }
            TargetsHit.Clear();
        }
        public void VisualizeFiring(Vector3 targetPosition)
        {
            if (canFire)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, targetPosition);

                canFire = false;

                _coroutineFiring = StartCoroutine(FiringCor());
            }
        }

        private IEnumerator FiringCor()
        {
            yield return _waitForFiring;

            canFire = true;
            _lineRenderer.enabled = false;
        }
        
    }
}
