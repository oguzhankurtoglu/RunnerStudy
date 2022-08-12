using System;
using Cinemachine;
using DG.Tweening;
using Script.State;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        public Camera mainCamera;
        public Character player;
        private State.State _currentState;


        public void SetState(State.State state)
        {
            _currentState = state;
            StartCoroutine(_currentState.Start());
        }

        private void Awake()
        {
            player = FindObjectOfType<Character>();
            mainCamera = Camera.main;
            SetState(new StateStart(this));
        }

        public void LevelStart()
        {
            gameState = GameState.Start;
        }
    }
}