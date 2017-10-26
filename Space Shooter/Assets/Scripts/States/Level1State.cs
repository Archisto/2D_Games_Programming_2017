using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.States
{
    class Level1State : GameStateBase
    {
        public override string SceneName
        {
            get
            {
                return "Level1";
            }
        }

        public override GameStateType StateType
        {
            get
            {
                return GameStateType.Level1;
            }
        }

        /// <summary>
        /// The constructor. Called right after the object is instantiated.
        /// </summary>
        public Level1State()
        {
            AddTargetState(GameStateType.Level2);
            AddTargetState(GameStateType.GameOver);
        }
    }
}
