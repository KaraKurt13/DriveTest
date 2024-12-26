using Assets.Scripts.Helpers;
using Assets.Scripts.Main;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private void Update()
        {
            if (GameEngine.IsMultiplayerGame)
            {
                if (!_leaderboardIsReady && GameEngine.PlayersResultsReady())
                    InitLeaderboard();
            }
        }

        public void Draw(int score, int earnedCash)
        {
            _playerScore.text = $"Your score: {score}!";
            _earnedMoney.text = $"Earned money: {earnedCash}$";
            _doubleReward.onClick.AddListener(() => IronSourceManager.Instance.DoubleReward(earnedCash));

            gameObject.SetActive(true);
        }

        #region Online
        [SerializeField]
        private GameObject _playerResultPrefab;

        [SerializeField]
        private GameObject _leaderboard;

        [SerializeField]
        private Transform _resultsContainer;

        [SerializeField]
        private Button _leaderboardButton;

        private bool _leaderboardIsReady;

        public void DrawLeaderboard()
        {
            _leaderboard.SetActive(true);
        }

        public void HideLeaderboard()
        {
            _leaderboard.SetActive(false);
        }

        private void InitLeaderboard()
        {
            var results = new List<KeyValuePair<string, int>>();
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.TryGetValue("Score", out var score))
                {
                    results.Add(new KeyValuePair<string, int>(player.NickName, (int)score));
                }
            }

            var sortedResults = results.OrderByDescending(t => t.Value).ToArray();

            for (int i = 1; i <= results.Count; i++)
            {
                var inst = Instantiate(_playerResultPrefab, _resultsContainer).GetComponent<PlayerResultSubcomponent>();
                var result = sortedResults[i - 1];
                inst.PlayerName.text = $"{i}|{result.Key}";
                inst.Score.text = result.Value.ToString();
            }
            _leaderboardIsReady = true;
            _leaderboardButton.interactable = true;
        }
        #endregion Online
    }
}