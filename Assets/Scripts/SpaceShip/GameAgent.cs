using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class GameAgent : MonoBehaviour
    {
        public enum Fraction
        {
            Player,
            Allies,
            Enemies
        }

        public Fraction shipFraction;
    }
}