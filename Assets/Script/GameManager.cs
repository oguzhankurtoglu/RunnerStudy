using System;
using UnityEngine;

namespace Script
{
    public enum GameState
    {
        Start,
        Running,
        Fail,
        BeforeFinish,
        Success
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Start;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)&& gameState==GameState.Start)
            {
                gameState = GameState.Running;
            }
        }
    }
}