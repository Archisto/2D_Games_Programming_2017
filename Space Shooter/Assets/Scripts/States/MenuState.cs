using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.States
{
    class MenuState : GameStateBase
    {
        public override string SceneName
        {
            get
            {
                return "MainMenu";
            }
        }

        public override GameStateType StateType
        {
            get
            {
                return GameStateType.MainMenu;
            }
        }

        /// <summary>
        /// The constructor. Called right after the object is instantiated.
        /// </summary>
        public MenuState()
        {
            AddTargetState(GameStateType.Level1);
        }

        public override void Activate()
        {
            base.Activate();

            GameManager.Instance.Reset();
        }
    }
}
