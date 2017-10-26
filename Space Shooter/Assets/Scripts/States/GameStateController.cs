using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.States
{
    public static class GameStateController
    {
        private static List<GameStateBase> gameStates =
            new List<GameStateBase>();

        public static GameStateBase CurrentState { get; private set; }

        static GameStateController()
        {
            if (!AddStartingState(new MenuState()))
            {
                Debug.LogError("Couldn't add a starting state.");
                return;
            }

            gameStates.Add(new Level1State());
            gameStates.Add(new Level2State());
        }

        public static bool PerformTransition(GameStateType targetStateType)
        {
            if ( !CurrentState.IsValidTargetState(targetStateType) )
            {
                return false;
            }

            GameStateBase state = GetStateByType(targetStateType);

            if (state == null)
            {
                return false;
            }

            CurrentState.Deactivate();
            CurrentState = state;
            CurrentState.Activate();

            return true;
        }

        private static bool AddStartingState(GameStateBase startingState)
        {
            foreach (GameStateBase gameState in gameStates)
            {
                if (gameState.StateType == startingState.StateType)
                {
                    return false;
                }
            }

            gameStates.Add(startingState);

            CurrentState = startingState;
            CurrentState.Activate();

            return true;
        }

        public static GameStateBase GetStateByType(GameStateType targetStateType)
        {
            GameStateBase result = null;

            foreach (GameStateBase gameState in gameStates)
            {
                if (gameState.StateType == targetStateType)
                {
                    result = gameState;
                    break;
                }
            }

            return result;
        }
    }
}
