using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.States
{
    class Level2State : GameStateBase
    {
        public override string SceneName
        {
            get
            {
                return "Level2";
            }
        }

        public override GameStateType StateType
        {
            get
            {
                return GameStateType.Level2;
            }
        }

        /// <summary>
        /// The constructor. Called right after the object is instantiated.
        /// </summary>
        public Level2State()
        {
            AddTargetState(GameStateType.GameOver);
        }
    }
}
