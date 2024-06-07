using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class SpaceShip : MonoBehaviour
    {
        public GameAgent shipAgent;
        public IInputShipMovement InputShipMovement;
        public IInputShipWeapons InputShipWeapons;

        void OnEnable()
        {
            if (shipAgent == null)
                shipAgent = GetComponent<GameAgent>();
            //Input
            if (InputShipMovement == null)
                InputShipMovement = GetComponent<IInputShipMovement>();
            if (InputShipWeapons == null)
                InputShipWeapons = GetComponent<IInputShipWeapons>();
        }
    }
}