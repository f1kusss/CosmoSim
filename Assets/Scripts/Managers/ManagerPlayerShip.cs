using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class ManagerPlayerShip : MonoBehaviour
    {
        [SerializeField] private int _currentShipID;
        [SerializeField] private Transform _shipVisual;
        [SerializeField] private GameObject _currentShip;
        [SerializeField] private List<GameObject> _shipPrefs = new List<GameObject>();

        private void Start()
        {
            ChangeShip(0);
        }

        void Update()
        {
            if (Input.GetButtonDown("ShipChange"))
            {
                ChangeShipToNext();
            }
        }

        void ChangeShipToNext()
        {
            _currentShipID++;
            if (_currentShipID == _shipPrefs.Count)
            {
                _currentShipID = 0;
            }

            ChangeShip(_currentShipID);
        }

        void ChangeShip(int id)
        {
            Destroy(_currentShip);
            _currentShip = Instantiate(_shipPrefs[_currentShipID], _shipVisual);
        }
    }
}