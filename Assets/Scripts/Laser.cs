using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cosmos_6
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        public bool canFire;
        public float MaxDistance = 100.0f;

        private Coroutine _coroutineFiring;
        private WaitForSeconds _waitForFiring;
        [SerializeField] private float _waitTimeFiring = 0.1f;

        [Header("Links")]
        [SerializeField] private LineRenderer _lineRenderer;
        void Start()
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
            _waitForFiring = new WaitForSeconds(_waitTimeFiring);
            _lineRenderer.enabled = false;
            canFire = true;
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 targetPosition = FireWeapon(transform.position + transform.forward * MaxDistance);
                VisualizeFireWeapon(targetPosition);
            }
        }
        public Vector3 FireWeapon(Vector3 targetPosition)
        {
            RaycastHit hitInfo;
            var direction = targetPosition - transform.position;
            if(Physics.Raycast(transform.position, direction, out hitInfo, MaxDistance))
            {
                var targetHit = hitInfo.transform;
                if (targetHit != null)
                {
                    Debug.Log($"Fire.WeapontargetHit:{targetHit.name}");
                    //Destroy(targetHit.gameObject);
                    return targetHit.position;
                }
            }
            return targetPosition;
        }
        public void VisualizeFireWeapon(Vector3 targetPosition)
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
