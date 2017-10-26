using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceShooter.States;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameStateType nextState;

        public void StartGame()
        {
            GameStateController.PerformTransition(nextState);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
