using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameComponentsController : MonoBehaviour
    {
        public PlayerStatsComponent PlayerStatsComponent;

        public GarageComponent GarageComponent;

        public GameResultsSubcomponent GameResultsSubcomponent;

        public WaitingScreenSubcomponent WaitingScreen;
    }
}