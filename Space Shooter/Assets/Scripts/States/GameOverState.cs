using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.States
{
    class GameOverState : GameStateBase
    {
        public override string SceneName
        {
            get
            {
                return "GameOver";
            }
        }

        public override GameStateType StateType
        {
            get
            {
                return GameStateType.GameOver;
            }
        }

        /// <summary>
        /// The constructor. Called right after the object is instantiated.
        /// </summary>
        public GameOverState()
        {
            AddTargetState(GameStateType.MainMenu);
        }
    }
}
