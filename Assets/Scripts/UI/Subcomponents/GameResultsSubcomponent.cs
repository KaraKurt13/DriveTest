using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameResultsSubcomponent : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _playerScore, _earnedMoney;

        public void Draw(int score, int earnedCash)
        {
            _playerScore.text = $"Your score: {score}!";
            _earnedMoney.text = $"Earned money: {earnedCash}$";
            gameObject.SetActive(true);
        }
    }
}