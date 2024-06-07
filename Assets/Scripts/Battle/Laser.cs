using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour, IWeapons
    {
        [Header("Settings")] [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _fireDistance = 100;
        [SerializeField] private float _fireRate = 0.1f;
        [SerializeField] private float _damage;
        private Coroutine _coroutineFire;
        private WaitForSeconds _waitForFire;
        [SerializeField] private ShipWeapons _shipWeapons;
        public List<IDamageable> TargetsHit = new List<IDamageable>();
        [Header("Inner")] [SerializeField] private bool _canFire;
        [Header("Visual FX")] [SerializeField] private float _lineRenAnimSpeed = 1f;

        [SerializeField] private float _lineRenAnimDelta = 0f;

        //ExtansicData
        private DataWeaponExtrinsic _dataWeaponExtrinsic;

        private void Awake()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();
            if (_shipWeapons == null)
                _shipWeapons = GetComponentInParent<ShipWeapons>();
            Debug.Log(_shipWeapons);
        }

        // Start is called before the first frame update
        void Start()
        {
            // _lineRenderer.enabled = false;
            _canFire = true;
            _waitForFire = new WaitForSeconds(_fireRate);
        }

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic)
        {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
        }

        void Update()
        {
            if (!_lineRenderer.enabled)
                return;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenAnimDelta += Time.deltaTime;
            if (_lineRenAnimDelta > 1f)
            {
                _lineRenAnimDelta = 0f;
            }

            _lineRenderer.material.SetTextureOffset("_MainTex",
                new Vector2((_lineRenAnimSpeed * _lineRenAnimDelta), 0f));

            //_lineRenderer.SetPosition(1, targetPosition);
        }

        // Update is called once per frame


        public Vector3 FireWeapon(Vector3 targetPosition)
        {
            if (!_canFire)
                return Vector3.zero;
            RaycastHit hitInfo;
            var dir = targetPosition - transform.position;
            if (Physics.Raycast(transform.position, dir, out hitInfo, _fireDistance))
            {
                var targetHit = hitInfo.transform;
                var impactPoint = hitInfo.point;
                if (targetHit != null)
                {
                    Debug.Log($"FireWeapon. targetHit = {targetHit.name}");
                    var damageableHit = targetHit.GetComponent<IDamageable>();
                    if (damageableHit != null)
                    {
                        TargetsHit.Add(damageableHit);
                        Damage(_damage, targetHit.position, _dataWeaponExtrinsic.GameAgent);
                    }

                    VisualizeFireWeapon(impactPoint);

                    Debug.Log("Laser FIreWeapon");
                    return targetHit.position;
                }
            }

            VisualizeFireWeapon(transform.position + dir.normalized * _fireDistance);

            return targetPosition;
        }

        private void Damage(float damage, Vector3 targetHitPosition, GameAgent sender)
        {
            foreach (var targetHit in TargetsHit)
            {
                targetHit.TakeDamage(damage, targetHitPosition, sender);
            }

            TargetsHit.Clear();
        }

        public void VisualizeFireWeapon(Vector3 targetPosition)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, targetPosition);
            _canFire = false;
            _coroutineFire = StartCoroutine(FireCor());
        }

        private IEnumerator FireCor()
        {
            yield return _waitForFire;
            _canFire = true;
            yield return _waitForFire;
            if (_canFire)
                _lineRenderer.enabled = false;
        }
    }
}