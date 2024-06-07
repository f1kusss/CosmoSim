using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cosmosim
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement multipliers")] [SerializeField]
        private float _rotPitchSpeed = 75000f;

        [SerializeField] private float _rotYawSpeed = 90000f;
        [SerializeField] private float _rotRollSpeed = 50000f;
        [SerializeField] private float _moveSpeed = 50000f;

        [Header("Drag multipliers")] [SerializeField] [Range(0.5f, 50f)]
        private float _proportionalAngularDrag = 5f;

        [Range(10f, 1000f)] [SerializeField] private float _proportionalDrag = 100f;

        [Header("Links")] [SerializeField] private SpaceShip SpaceShip;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Engine[] _engines;

        void Start()
        {
        }

        void FixedUpdate()
        {
            Turn(SpaceShip.InputShipMovement.CurrentInputRotatePitch,
                SpaceShip.InputShipMovement.CurrentInputRotateYaw,
                SpaceShip.InputShipMovement.CurrentInputRotateRoll);
            Move(SpaceShip.InputShipMovement.CurrentInputMove);
        }


        private void Turn(float inputPitch, float inputYaw, float inputRoll)
        {
            if (!Mathf.Approximately(0f, inputPitch))
                _rigidbody.AddTorque(transform.right * (-inputPitch * _rotPitchSpeed * Time.fixedDeltaTime));
            if (!Mathf.Approximately(0f, inputYaw))
                _rigidbody.AddTorque(transform.up * (inputYaw * _rotYawSpeed * Time.fixedDeltaTime));
            if (!Mathf.Approximately(0f, inputRoll))
                _rigidbody.AddTorque(transform.forward * (inputRoll * _rotRollSpeed * Time.fixedDeltaTime));
            if (inputRoll == 0 & inputYaw == 0 & inputPitch == 0)
                _rigidbody.AddTorque(-_rigidbody.angularVelocity * (_proportionalAngularDrag * Time.fixedDeltaTime));
        }

        private void Move(float inputMove)
        {
            Vector3 resultingThrust;
            resultingThrust = new Vector3();
            foreach (var engine in _engines)
            {
                resultingThrust += engine.Thrust(inputMove);
            }


            _rigidbody.AddForce(resultingThrust * (_moveSpeed * Time.fixedDeltaTime));
            if (inputMove == 0)
                _rigidbody.AddForce(_rigidbody.velocity * (_proportionalDrag * Time.fixedDeltaTime));
        }
    }
}