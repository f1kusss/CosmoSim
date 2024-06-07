using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cosmosim
{
    public class ManagerAchivements : SingletonManager<ManagerAchivements>
    {
        // Start is called before the first frame update
        void OnEnable()
        {
            ManagerScore.Instance.OnNewScore += CheckAchievements;
        }

        private void OnDisable()
        {
            ManagerScore.Instance.OnNewScore -= CheckAchievements;
        }

        // Update is called once per frame
        void CheckAchievements(int newScore)
        {
            if (newScore >= 9)
                Debug.Log(
                    "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!You are chamPIVON!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}