using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class Engine : MonoBehaviour
    {
        [Serializable]
        public class EngineVisuals
        {
            [SerializeField] private ParticleSystem ParticleSystem;

            [Header("Settings")] [SerializeField] private float _psEmmiterMax = 50f;
            [SerializeField] private float _psEmmiterMin = 10f;
            [SerializeField] private float _visualizationLerpRate = 0.25f;

            [Header("CurrValues")] [SerializeField] private float _psEmmiterCurr = 50f;
            [SerializeField] private float _visualizationCurrNorm = 0f; // 0-1


            public void VisualiseThrusts(float inputMove)
            {
                var emission = ParticleSystem.emission;

                _visualizationCurrNorm = Mathf.Lerp(_visualizationCurrNorm, inputMove, _visualizationLerpRate);
                _psEmmiterCurr = _psEmmiterMax * _visualizationCurrNorm;
                emission.rateOverTime = Mathf.Max(_psEmmiterMin, _psEmmiterCurr);
            }
        }

        [SerializeField] private float _moveSpeed = 100f;
        [SerializeField] private List<EngineVisuals> _engineVisualsList;


        // Update is called once per frame
        public Vector3 Thrust(float inputMove)
        {
            VisualiseThrusts(inputMove);
            var calculatedThrust = inputMove * _moveSpeed;
            return -transform.forward * (calculatedThrust * Time.fixedDeltaTime);
        }

        private void VisualiseThrusts(float inputMove)
        {
            foreach (var ev in _engineVisualsList)
            {
                ev.VisualiseThrusts(inputMove);
            }
        }
    }
}