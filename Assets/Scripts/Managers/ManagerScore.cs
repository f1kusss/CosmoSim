using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Cosmosim
{
    public class ManagerScore : SingletonManager<ManagerScore>
    {
        public int Score;
        public TextMeshProUGUI ScoreText;
        public Action<int> OnAddScore;
        public Action<int> OnNewScore;

        //[SerializeField] private UnityEvent<int> 

        void Start()
        {
            if (ScoreText == null)
                Debug.Log("ManagerScore. ScoreText==null");
            else
                ScoreText.text = Score.ToString();
        }

        private void OnEnable()
        {
            OnAddScore += IncreaseScore;
        }


        private void OnDisable()
        {
            OnAddScore -= IncreaseScore;
        }

        public  void AddScore(int scoreDate)
        {
            OnAddScore?.Invoke(scoreDate);
        }

        public void IncreaseScore(int scoreData)
        {
            Score += scoreData;
            ScoreText.text = Score.ToString();
            OnNewScore?.Invoke(Score);
        }
    }
}