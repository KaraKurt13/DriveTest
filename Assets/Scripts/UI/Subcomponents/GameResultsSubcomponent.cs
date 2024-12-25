using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameResultsSubcomponent : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerScore, _earnedMoney;

        [SerializeField]
        private Button _doubleReward;

        public void Draw(int score, int earnedCash)
        {
            _playerScore.text = $"Your score: {score}!";
            _earnedMoney.text = $"Earned money: {earnedCash}$";
            _doubleReward.onClick.AddListener(() => IronSourceManager.Instance.DoubleReward(earnedCash));

            gameObject.SetActive(true);
        }
    }
}