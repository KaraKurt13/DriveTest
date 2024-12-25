using Assets.Scripts.Helpers;
using Assets.Scripts.Main;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerStatsComponent : ComponentBase
    {
        public GameEngine Engine;

        [SerializeField]
        private TextMeshProUGUI _score, _speed, _timeLeft;

        [SerializeField]
        private StatsTracker _playerStats;

        public void Init(StatsTracker playerStatsTracker)
        {
            _playerStats = playerStatsTracker;
            _speedBuilder = new();
            _timeLeftBuilder = new();
        }

        private void Update()
        {
            if (Engine.IsGameRunning)
                UpdateStats();
        }

        private StringBuilder _speedBuilder, _timeLeftBuilder;

        private void UpdateStats()
        {
            var speed = (_playerStats.CarSpeed * Constants.MpsToKmh).ToString("F0");
            var ticksTillEnd = TimeHelper.TicksToSeconds(Engine.TicksTillEnd).ToString("F1");
            _speedBuilder.Clear()
                .Append(speed)
                .Append(" km/h");
            _timeLeftBuilder.Clear()
                .Append(ticksTillEnd)
                .Append(" sec.");
            _speed.text = _speedBuilder.ToString();
            _score.text = _playerStats.Score.ToString();
            _timeLeft.text = _timeLeftBuilder.ToString();
        }
    }
}